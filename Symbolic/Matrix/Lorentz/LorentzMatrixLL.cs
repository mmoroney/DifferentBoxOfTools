using Symbolic.Utilities;
using Symbolic.Vector.Lorentz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Lorentz
{
    public class LorentzMatrixLL : LorentzMatrixBase<Symbol, LorentzMatrixLL, LorentzMatrixUU>
    {
        public LorentzMatrixLL(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public LorentzMatrixLL(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override LorentzMatrixLL Create(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixLL(initializer);
        }

        protected override LorentzMatrixUU CreateInvert(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixUU(initializer);
        }

        public static LorentzVectorL operator *(LorentzMatrixLL lhs, LorentzVectorU rhs)
        {
            return new LorentzVectorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorL operator *(LorentzVectorU lhs, LorentzMatrixLL rhs)
        {
            return new LorentzVectorL(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorOperatorL operator *(LorentzMatrixLL lhs, LorentzVectorOperatorU rhs)
        {
            return new LorentzVectorOperatorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static LorentzVectorL operator *(LorentzVectorOperatorU lhs, LorentzMatrixLL rhs)
        {
            return new LorentzVectorL(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static LorentzMatrixLU operator *(LorentzMatrixLL lhs, LorentzMatrixUU rhs)
        {
            return new LorentzMatrixLU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static LorentzMatrixLL operator *(LorentzMatrixLL lhs, LorentzMatrixUL rhs)
        {
            return new LorentzMatrixLL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
