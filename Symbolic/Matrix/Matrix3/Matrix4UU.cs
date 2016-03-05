using Symbolic.Utilities;
using Symbolic.Vector.Vector3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix3
{
    public class Matrix3UU : Matrix3Base<Symbol, Matrix3UU, Matrix3LL>
    {
        public Matrix3UU(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public Matrix3UU(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2)) { }

        protected override Matrix3UU Create(Func<int, int, Symbol> initializer)
        {
            return new Matrix3UU(initializer);
        }

        public static Vector3U operator *(Matrix3UU lhs, Vector3L rhs)
        {
            return new Vector3U(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static Vector3U operator *(Vector3L lhs, Matrix3UU rhs)
        {
            return new Vector3U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Vector3OperatorU operator *(Matrix3UU lhs, Vector3OperatorL rhs)
        {
            return new Vector3OperatorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static Vector3U operator *(Vector3OperatorL lhs, Matrix3UU rhs)
        {
            return new Vector3U(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static Matrix3UL operator *(Matrix3UU lhs, Matrix3LL rhs)
        {
            return new Matrix3UL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static Matrix3UU operator *(Matrix3UU lhs, Matrix3LU rhs)
        {
            return new Matrix3UU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
