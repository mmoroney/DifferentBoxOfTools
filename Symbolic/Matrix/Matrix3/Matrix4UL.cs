using Symbolic.Utilities;
using Symbolic.Vector.Vector3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix3
{
    public class Matrix3UL : Matrix3Base<Symbol, Matrix3UL, Matrix3LU>
    {
        public Matrix3UL(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix3UL(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2)) { }

        protected override Matrix3UL Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix3UL(initializer);
        }

        public static Vector3U operator *(Matrix3UL lhs, Vector3U rhs)
        {
            return new Vector3U(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector3L operator *(Vector3L lhs, Matrix3UL rhs)
        {
            return new Vector3L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector3OperatorU operator *(Matrix3UL lhs, Vector3OperatorU rhs)
        {
            return new Vector3OperatorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector3L operator *(Vector3OperatorL lhs, Matrix3UL rhs)
        {
            return new Vector3L(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix3UL operator *(Matrix3UL lhs, Matrix3UL rhs)
        {
            return new Matrix3UL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix3UU operator *(Matrix3UL lhs, Matrix3UU rhs)
        {
            return new Matrix3UU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
