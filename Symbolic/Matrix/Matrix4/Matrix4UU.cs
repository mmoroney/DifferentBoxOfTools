using Symbolic.Utilities;
using Symbolic.Vector.Vector4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix4
{
    public class Matrix4UU : Matrix4Base<Symbol, Matrix4UU, Matrix4LL>
    {
        public static Matrix4UU Zero { get; private set; }

        static Matrix4UU()
        {
            Matrix4UU.Zero = new Matrix4UU((i, j) => Symbol.Zero);
        }

        public Matrix4UU(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix4UU(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override Matrix4UU Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix4UU(initializer);
        }

        public static Vector4U operator *(Matrix4UU lhs, Vector4L rhs)
        {
            return new Vector4U(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector4U operator *(Vector4L lhs, Matrix4UU rhs)
        {
            return new Vector4U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector4OperatorU operator *(Matrix4UU lhs, Vector4OperatorL rhs)
        {
            return new Vector4OperatorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector4U operator *(Vector4OperatorL lhs, Matrix4UU rhs)
        {
            return new Vector4U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix4UL operator *(Matrix4UU lhs, Matrix4LL rhs)
        {
            return new Matrix4UL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix4UU operator *(Matrix4UU lhs, Matrix4LU rhs)
        {
            return new Matrix4UU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix4UU operator *(Symbol lhs, Matrix4UU rhs)
        {
            return new Matrix4UU((i, j) => lhs * rhs[i, j]);
        }

        public static Matrix4UU operator /(Matrix4UU lhs, int rhs)
        {
            Rational rational = new Rational(rhs);
            return new Matrix4UU((i, j) => lhs[i, j] / rhs);
        }
    }
}
