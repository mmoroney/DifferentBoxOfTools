using Symbolic.Matrix;
using Symbolic.Operators;
using Symbolic.Tensor;
using Symbolic.Utilities;
using Symbolic.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Manifold
{
    public abstract class ManifoldBase<MatrixLL, MatrixUU, VectorOperatorL, VectorOperatorU, Tensor4D>
        where MatrixLL : MetricMatrixBase<Symbol, MatrixLL, MatrixUU>
        where MatrixUU : MetricMatrixBase<Symbol, MatrixUU, MatrixLL>
        where VectorOperatorL : VectorBase<Operator, VectorOperatorL, VectorOperatorU>
        where VectorOperatorU : VectorBase<Operator, VectorOperatorU, VectorOperatorL>
        where Tensor4D : Tensor4DBase<Symbol>
    {
        public MatrixLL CovariantMetric { get; private set; }
        public MatrixUU ContravariantMetric { get; private set; }
        public VectorOperatorL Del { get; private set; }

        private Tensor4D riemannTensor;
        private MatrixLL ricciTensor;
        private Symbol ricciScalar;
        private Symbol[, ,] christoffelSymbols;

        protected ManifoldBase(MatrixLL metric, VectorOperatorL del)
        {
            this.CovariantMetric = metric;
            this.ContravariantMetric = this.CreateContravariantMetric(
                MatrixUtilities.MatrixInverse((i, j) => this.CovariantMetric[i, j], Symbol.Zero, x => 1 / x, (x, y) => x == y));
            this.Del = del;

            int length = this.Size;
            this.christoffelSymbols = new Symbol[length, length, length];
        }

        public int Size
        {
            get
            {
                return this.CovariantMetric.Size;
            }
        }

        public Tensor4D RiemannTensor
        {
            get
            {
                if (this.riemannTensor == null)
                {
                    this.riemannTensor = this.CreateRiemannTensor((i, j, k, l) =>
                    {
                        Symbol symbol = this.Del[k] * this.ChristoffelSymbol(i, j, l)
                        - this.Del[l] * this.ChristoffelSymbol(i, j, k);

                        for (int m = 0; m < this.Size; m++)
                        {
                            symbol = symbol + this.ChristoffelSymbol(i, m, k) * this.ChristoffelSymbol(m, j, l);
                            symbol = symbol - this.ChristoffelSymbol(i, m, l) * this.ChristoffelSymbol(m, j, k);
                        }

                        return symbol;
                    });
                }

                return this.riemannTensor;
            }
        }

        public MatrixLL RicciTensor
        {
            get
            {
                if (this.ricciTensor == null)
                {
                    this.ricciTensor = this.CreateRicciTensor((i, j) =>
                    {
                        Symbol symbol = Symbol.Zero;
                        for (int k = 0; k < this.Size; k++)
                        {
                            symbol = symbol + this.RiemannTensor[k, i, k, j];
                        }
                        return symbol;
                    });
                }
                return this.ricciTensor;
            }
        }

        public Symbol RicciScalar 
        {
            get
            {
                if (this.ricciScalar == null)
                {
                    this.ricciScalar = this.ContravariantMetric.Dot(this.RicciTensor);
                }
                return this.ricciScalar;
            }
        }

        public Symbol ChristoffelSymbol(int covariant, int contravariant1, int contravariant2)
        {
            Symbol symbol = this.christoffelSymbols[covariant, contravariant1, contravariant2];

            if (symbol != null)
            {
                return symbol;
            }

            symbol = Symbol.Zero;

            for (int i = 0; i < this.Del.Size; i++)
            {
                symbol = symbol + this.ContravariantMetric[covariant, i] * (
                    this.Del[contravariant2] * this.CovariantMetric[contravariant1, i]
                    + this.Del[contravariant1] * this.CovariantMetric[contravariant2, i]
                    - this.Del[i] * this.CovariantMetric[contravariant1, contravariant2]) / 2;
            }

            this.christoffelSymbols[covariant, contravariant1, contravariant2] = symbol;
            this.christoffelSymbols[covariant, contravariant2, contravariant1] = symbol;
            
            return symbol;
        }

        protected abstract Tensor4D CreateRiemannTensor(Func<int, int, int, int, Symbol> initializer);
        protected abstract MatrixLL CreateRicciTensor(Func<int, int, Symbol> initializer);
        protected abstract MatrixUU CreateContravariantMetric(Func<int, int, Symbol> initializer);
    }
}
