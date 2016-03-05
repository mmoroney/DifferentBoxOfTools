using Symbolic.Utilities;
using Symbolic.Vector.Lorentz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Lorentz
{
    public class LorentzMatrixLU : LorentzMatrixBase<Symbol, LorentzMatrixLU, LorentzMatrixUL>
    {
        public LorentzMatrixLU(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public LorentzMatrixLU(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override LorentzMatrixLU Create(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixLU(initializer);
        }

        protected override LorentzMatrixUL CreateInvert(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixUL(initializer);
        }

        public static LorentzVectorL operator *(LorentzMatrixLU lhs, LorentzVectorL rhs)
        {
            return new LorentzVectorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorU operator *(LorentzVectorU lhs, LorentzMatrixLU rhs)
        {
            return new LorentzVectorU(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorOperatorL operator *(LorentzMatrixLU lhs, LorentzVectorOperatorL rhs)
        {
            return new LorentzVectorOperatorL(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static LorentzVectorU operator *(LorentzVectorOperatorU lhs, LorentzMatrixLU rhs)
        {
            return new LorentzVectorU(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static LorentzMatrixLU operator *(LorentzMatrixLU lhs, LorentzMatrixLU rhs)
        {
            return new LorentzMatrixLU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static LorentzMatrixLL operator *(LorentzMatrixLU lhs, LorentzMatrixLL rhs)
        {
            return new LorentzMatrixLL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }    
    }
}
