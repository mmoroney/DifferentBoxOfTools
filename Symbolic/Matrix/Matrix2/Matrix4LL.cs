using Symbolic.Utilities;
using Symbolic.Vector.Vector2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix2
{
    public class Matrix2LL : Matrix2Base<Symbol, Matrix2LL, Matrix2UU>
    {
        public Matrix2LL(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix2LL(Symbol diagonal0, Symbol diagonal1)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1)) { }

        protected override Matrix2LL Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix2LL(initializer);
        }

        public static Vector2L operator *(Matrix2LL lhs, Vector2U rhs)
        {
            return new Vector2L(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector2L operator *(Vector2U lhs, Matrix2LL rhs)
        {
            return new Vector2L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector2OperatorL operator *(Matrix2LL lhs, Vector2OperatorU rhs)
        {
            return new Vector2OperatorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector2L operator *(Vector2OperatorU lhs, Matrix2LL rhs)
        {
            return new Vector2L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix2LU operator *(Matrix2LL lhs, Matrix2UU rhs)
        {
            return new Matrix2LU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix2LL operator *(Matrix2LL lhs, Matrix2UL rhs)
        {
            return new Matrix2LL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
