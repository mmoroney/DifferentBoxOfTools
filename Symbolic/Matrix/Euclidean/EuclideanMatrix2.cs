using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbolic.Vector;
using Symbolic.Operators;
using Symbolic.Utilities;
using Symbolic.Vector.Euclidean;
using Symbolic.Algebra;

namespace Symbolic.Matrix
{
    public class EuclideanMatrix2 : MatrixBase<Symbol, EuclideanMatrix2, EuclideanMatrix2>
    {
        private static EuclideanMatrix2 one;

        static EuclideanMatrix2()
        {
            EuclideanMatrix2.one = new EuclideanMatrix2(MatrixUtilities.CreateIdentityMatrix(Symbol.Zero, Symbol.One));
        }

        public static EuclideanMatrix2 One
        {
            get { return EuclideanMatrix2.one; }
        }

        public EuclideanMatrix2(Func<int, int, Symbol> initializer) : base(Symbol.Operations, 2, initializer) { }
        public EuclideanMatrix2(Symbol diagonal0, Symbol diagonal1) : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1)) { }

        public EuclideanMatrix2 Value
        {
            get
            {
                return new EuclideanMatrix2((i, j) => new Constant(this[i, j].Value));
            }
        }

        protected override EuclideanMatrix2 Create(Func<int, int, Symbol> initializer)
        {
            return new EuclideanMatrix2(initializer);
        }

        public EuclideanMatrix2 Diff(Variable variable)
        {
            return new EuclideanMatrix2((i, j) => variable.Derivative * this[i, j]);
        }

        public static EuclideanMatrix2 operator *(EuclideanMatrix2 lhs, int rhs)
        {
            return new EuclideanMatrix2((i, j) => lhs[i, j] * rhs);
        }

        public static EuclideanMatrix2 operator *(int lhs, EuclideanMatrix2 rhs)
        {
            return rhs * lhs;
        }

        public static EuclideanMatrix2 operator /(EuclideanMatrix2 lhs, int rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
