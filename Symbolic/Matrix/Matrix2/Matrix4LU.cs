using Symbolic.Utilities;
using Symbolic.Vector.Vector2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix2
{
    public class Matrix2LU : Matrix2Base<Symbol, Matrix2LU, Matrix2UL>
    {
        public Matrix2LU(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix2LU(Symbol diagonal0, Symbol diagonal1)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1)) { }

        protected override Matrix2LU Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix2LU(initializer);
        }

        public static Vector2L operator *(Matrix2LU lhs, Vector2L rhs)
        {
            return new Vector2L(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector2U operator *(Vector2U lhs, Matrix2LU rhs)
        {
            return new Vector2U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector2OperatorL operator *(Matrix2LU lhs, Vector2OperatorL rhs)
        {
            return new Vector2OperatorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector2U operator *(Vector2OperatorU lhs, Matrix2LU rhs)
        {
            return new Vector2U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix2LU operator *(Matrix2LU lhs, Matrix2LU rhs)
        {
            return new Matrix2LU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix2LL operator *(Matrix2LU lhs, Matrix2LL rhs)
        {
            return new Matrix2LL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
