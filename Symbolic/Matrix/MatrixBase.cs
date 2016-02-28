using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix
{
    public abstract class MatrixBase<TScalar, TMatrix, TInvert>
        where TMatrix: MatrixBase<TScalar, TMatrix, TInvert>
        where TInvert: MatrixBase<TScalar, TInvert, TMatrix>
    {
        private TScalar[,] components;
        internal IOperations<TScalar> Operations { get; private set; }

        internal MatrixBase(IOperations<TScalar> operations, int length, Func<int, int, TScalar> initializer)
        {
            this.components = ArrayUtilities.Initialize(length, length, initializer);
            this.Operations = operations;
        }

        public int Size
        {
            get
            {
                return this.components.GetLength(0);
            }
        }

        public TScalar this[int i, int j]
        {
            get { return this.components[i, j]; }
        }

        public TScalar Dot(MatrixBase<TScalar, TInvert, TMatrix> matrix)
        {
            return MatrixUtilities.ScalarProduct((i, j) => this[i, j], (i, j) => matrix[i, j], this.Size, this.Operations);
        }

        protected abstract TMatrix Create(Func<int, int, TScalar> initializer);

        public TScalar Trace
        {
            get
            {
                TScalar trace = this.Operations.Zero;
                for (int i = 0; i < this.Size; i++)
                {
                    trace = this.Operations.Add(trace, this[i, i]);
                }
                return trace;
            }
        }

        protected string BuildMatrixString(Func<TScalar, string> toString)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    stringBuilder.Append(this[i, j]);
                    if (j != this.Size - 1)
                    {
                        stringBuilder.Append(" ");
                    }
                }
                if (i != this.Size - 1)
                {
                    stringBuilder.Append("\n");
                }
            }

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return this.BuildMatrixString(x => x.ToString());
        }

        public static TMatrix operator +(MatrixBase<TScalar, TMatrix, TInvert> lhs, MatrixBase<TScalar, TMatrix, TInvert> rhs)
        {
            return lhs.Create((i, j) => lhs.Operations.Add(lhs[i, j], rhs[i, j]));
        }

        public static TMatrix operator -(MatrixBase<TScalar, TMatrix, TInvert> lhs, MatrixBase<TScalar, TMatrix, TInvert> rhs)
        {
            return lhs.Create((i, j) => lhs.Operations.Subtract(lhs[i, j], rhs[i, j]));
        }

        public static TMatrix operator -(MatrixBase<TScalar, TMatrix, TInvert> matrix)
        {
            return matrix.Create((i, j) => matrix.Operations.Negative(matrix[i, j]));
        }

        public override bool Equals(object obj)
        {
            MatrixBase<TScalar, TMatrix, TInvert> rhs = obj as MatrixBase<TScalar, TMatrix, TInvert>;
            if (rhs == null)
            {
                return false;
            }

            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    if (!this.Operations.Compare(this[i, j], rhs[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator ==(MatrixBase<TScalar, TMatrix, TInvert> lhs, MatrixBase<TScalar, TMatrix, TInvert> rhs)
        {
            if (System.Object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(MatrixBase<TScalar, TMatrix, TInvert> lhs, MatrixBase<TScalar, TMatrix, TInvert> rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return this.BuildMatrixString(x => this.Operations.GetCanonicalString(x)).GetHashCode();
        }
    }
}
