using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbolic.Vector;
using Symbolic.Operators;
using Symbolic.Utilities;
using Symbolic.Vector.Lorentz;
using Symbolic.Algebra;

namespace Symbolic.Matrix
{
    public class EuclideanMatrix4 : MatrixBase<Symbol, EuclideanMatrix4, EuclideanMatrix4>
    {
        public static EuclideanMatrix4 One { get; private set; }

        static EuclideanMatrix4()
        {
            EuclideanMatrix4.One = new EuclideanMatrix4(MatrixUtilities.CreateIdentityMatrix(Symbol.Zero, Symbol.One));
        }

        public EuclideanMatrix4(Func<int, int, Symbol> initializer) : base(Symbol.Operations, 4, initializer) { }
        public EuclideanMatrix4(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3) : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override EuclideanMatrix4 Create(Func<int, int, Symbol> initializer)
        {
            return new EuclideanMatrix4(initializer);
        }
        
        public EuclideanMatrix4 Value
        {
            get
            {
                return new EuclideanMatrix4((i, j) => new Constant(this[i, j].Value));
            }
        }

        public EuclideanMatrix4 Diff(Variable variable)
        {
            return new EuclideanMatrix4((i, j) => variable.Derivative * this[i, j]);
        }

        public static EuclideanMatrix4 operator *(EuclideanMatrix4 lhs, int rhs)
        {
            return new EuclideanMatrix4((i, j) => lhs[i, j] * rhs);
        }

        public static EuclideanMatrix4 operator *(int lhs, EuclideanMatrix4 rhs)
        {
            return rhs * lhs;
        }

        public static EuclideanMatrix4 operator /(EuclideanMatrix4 lhs, int rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
