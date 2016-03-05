using Symbolic.Utilities;
using Symbolic.Vector.Vector4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix4
{
    public class Matrix4LL : Matrix4Base<Symbol, Matrix4LL, Matrix4UU>
    {
        public Matrix4LL(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix4LL(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override Matrix4LL Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix4LL(initializer);
        }

        public static Vector4L operator *(Matrix4LL lhs, Vector4U rhs)
        {
            return new Vector4L(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector4L operator *(Vector4U lhs, Matrix4LL rhs)
        {
            return new Vector4L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector4OperatorL operator *(Matrix4LL lhs, Vector4OperatorU rhs)
        {
            return new Vector4OperatorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector4L operator *(Vector4OperatorU lhs, Matrix4LL rhs)
        {
            return new Vector4L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix4LU operator *(Matrix4LL lhs, Matrix4UU rhs)
        {
            return new Matrix4LU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix4LL operator *(Matrix4LL lhs, Matrix4UL rhs)
        {
            return new Matrix4LL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
