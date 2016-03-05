using Symbolic.Utilities;
using Symbolic.Vector.Vector2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix2
{
    public class Matrix2UU : Matrix2Base<Symbol, Matrix2UU, Matrix2LL>
    {
        public Matrix2UU(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix2UU(Symbol diagonal0, Symbol diagonal1)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1)) { }

        protected override Matrix2UU Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix2UU(initializer);
        }

        public static Vector2U operator *(Matrix2UU lhs, Vector2L rhs)
        {
            return new Vector2U(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector2U operator *(Vector2L lhs, Matrix2UU rhs)
        {
            return new Vector2U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector2OperatorU operator *(Matrix2UU lhs, Vector2OperatorL rhs)
        {
            return new Vector2OperatorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector2U operator *(Vector2OperatorL lhs, Matrix2UU rhs)
        {
            return new Vector2U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix2UL operator *(Matrix2UU lhs, Matrix2LL rhs)
        {
            return new Matrix2UL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix2UU operator *(Matrix2UU lhs, Matrix2LU rhs)
        {
            return new Matrix2UU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
