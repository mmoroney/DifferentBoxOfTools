using Symbolic.Utilities;
using Symbolic.Vector.Vector3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix3
{
    public class Matrix3LU : Matrix3Base<Symbol, Matrix3LU, Matrix3UL>
    {
        public Matrix3LU(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix3LU(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2)) { }

        protected override Matrix3LU Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix3LU(initializer);
        }

        public static Vector3L operator *(Matrix3LU lhs, Vector3L rhs)
        {
            return new Vector3L(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector3U operator *(Vector3U lhs, Matrix3LU rhs)
        {
            return new Vector3U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector3OperatorL operator *(Matrix3LU lhs, Vector3OperatorL rhs)
        {
            return new Vector3OperatorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector3U operator *(Vector3OperatorU lhs, Matrix3LU rhs)
        {
            return new Vector3U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix3LU operator *(Matrix3LU lhs, Matrix3LU rhs)
        {
            return new Matrix3LU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix3LL operator *(Matrix3LU lhs, Matrix3LL rhs)
        {
            return new Matrix3LL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
