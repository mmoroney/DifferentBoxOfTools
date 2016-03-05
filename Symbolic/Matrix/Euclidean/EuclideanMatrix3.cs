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
    public class EuclideanMatrix3 : MatrixBase<Symbol, EuclideanMatrix3, EuclideanMatrix3>
    {
        public static EuclideanMatrix3 One { get; private set; }

        static EuclideanMatrix3()
        {
            EuclideanMatrix3.One = new EuclideanMatrix3(MatrixUtilities.CreateIdentityMatrix(Symbol.Zero, Symbol.One));
        }

        public EuclideanMatrix3(Func<int, int, Symbol> initializer) : base(Symbol.Operations, 3, initializer) { }
        public EuclideanMatrix3(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2) : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2)) { }

        protected override EuclideanMatrix3 Create(Func<int, int, Symbol> initializer)
        {
            return new EuclideanMatrix3(initializer);
        }

        public EuclideanMatrix3 Value
        {
            get
            {
                return new EuclideanMatrix3((i, j) => new Constant(this[i, j].Value));
            }
        }

        public EuclideanMatrix3 Diff(Variable variable)
        {
            return new EuclideanMatrix3((i, j) => variable.Derivative * this[i, j]);
        }

        public static EuclideanMatrix3 operator *(EuclideanMatrix3 lhs, int rhs)
        {
            return new EuclideanMatrix3((i, j) => lhs[i, j] * rhs);
        }

        public static EuclideanMatrix3 operator *(int lhs, EuclideanMatrix3 rhs)
        {
            return rhs * lhs;
        }

        public static EuclideanMatrix3 operator /(EuclideanMatrix3 lhs, int rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
