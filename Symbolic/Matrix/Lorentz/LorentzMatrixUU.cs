using Symbolic.Utilities;
using Symbolic.Vector.Lorentz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Lorentz
{
    public class LorentzMatrixUU : LorentzMatrixBase<Symbol, LorentzMatrixUU, LorentzMatrixLL>
    {
        public LorentzMatrixUU(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public LorentzMatrixUU(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override LorentzMatrixUU Create(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixUU(initializer);
        }

        protected override LorentzMatrixLL CreateInvert(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixLL(initializer);
        }

        public static LorentzVectorU operator *(LorentzMatrixUU lhs, LorentzVectorL rhs)
        {
            return new LorentzVectorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorU operator *(LorentzVectorL lhs, LorentzMatrixUU rhs)
        {
            return new LorentzVectorU(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorOperatorU operator *(LorentzMatrixUU lhs, LorentzVectorOperatorL rhs)
        {
            return new LorentzVectorOperatorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static LorentzVectorU operator *(LorentzVectorOperatorL lhs, LorentzMatrixUU rhs)
        {
            return new LorentzVectorU(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }
        
        public static LorentzMatrixUL operator *(LorentzMatrixUU lhs, LorentzMatrixLL rhs)
        {
            return new LorentzMatrixUL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static LorentzMatrixUU operator *(LorentzMatrixUU lhs, LorentzMatrixLU rhs)
        {
            return new LorentzMatrixUU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
