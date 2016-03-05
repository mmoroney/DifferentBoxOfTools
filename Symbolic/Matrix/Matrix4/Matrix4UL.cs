using Symbolic.Utilities;
using Symbolic.Vector.Vector4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix4
{
    public class Matrix4UL : Matrix4Base<Symbol, Matrix4UL, Matrix4LU>
    {
        public Matrix4UL(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix4UL(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override Matrix4UL Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix4UL(initializer);
        }

        public static Vector4U operator *(Matrix4UL lhs, Vector4U rhs)
        {
            return new Vector4U(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector4L operator *(Vector4L lhs, Matrix4UL rhs)
        {
            return new Vector4L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector4OperatorU operator *(Matrix4UL lhs, Vector4OperatorU rhs)
        {
            return new Vector4OperatorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector4L operator *(Vector4OperatorL lhs, Matrix4UL rhs)
        {
            return new Vector4L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix4UL operator *(Matrix4UL lhs, Matrix4UL rhs)
        {
            return new Matrix4UL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix4UU operator *(Matrix4UL lhs, Matrix4UU rhs)
        {
            return new Matrix4UU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
