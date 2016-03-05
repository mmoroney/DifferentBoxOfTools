using Symbolic.Operators;
using Symbolic.Utilities;
using Symbolic.Vector.Lorentz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Lorentz
{
    public class LorentzMatrixUL : LorentzMatrixBase<Symbol, LorentzMatrixUL, LorentzMatrixLU>
    {
        public static LorentzMatrixUL One { get; private set; }

        static LorentzMatrixUL()
        {
            LorentzMatrixUL.One = new LorentzMatrixUL(MatrixUtilities.CreateIdentityMatrix(Symbol.Zero, Symbol.One));
        }
        
        public LorentzMatrixUL(Func<int, int, Symbol> initializer) : base(Symbol.Operations, initializer) { }
        public LorentzMatrixUL(Symbol diagonal0, Symbol diagonal1, Symbol diagonal2, Symbol diagonal3)
            : this(MatrixUtilities.DiagonalInitializer(Symbol.Zero, diagonal0, diagonal1, diagonal2, diagonal3)) { }

        protected override LorentzMatrixUL Create(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixUL(initializer);
        }

        protected override LorentzMatrixLU CreateInvert(Func<int, int, Symbol> initializer)
        {
            return new LorentzMatrixLU(initializer);
        }

        public static LorentzVectorU operator *(LorentzMatrixUL lhs, LorentzVectorU rhs)
        {
            return new LorentzVectorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorL operator *(LorentzVectorL lhs, LorentzMatrixUL rhs)
        {
            return new LorentzVectorL(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }

        public static LorentzVectorOperatorU operator *(LorentzMatrixUL lhs, LorentzVectorOperatorU rhs)
        {
            return new LorentzVectorOperatorU(MatrixUtilities.MatrixVectorMultiply((i, j) => lhs[i, j], i => rhs[i], lhs.Size));
        }

        public static LorentzVectorL operator *(LorentzVectorOperatorL lhs, LorentzMatrixUL rhs)
        {
            return new LorentzVectorL(MatrixUtilities.VectorMatrixMultiply(i => lhs[i], (i, j) => rhs[i, j], lhs.Size));
        }

        public static LorentzMatrixUL operator *(LorentzMatrixUL lhs, LorentzMatrixUL rhs)
        {
            return new LorentzMatrixUL(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
        
        public static LorentzMatrixUU operator *(LorentzMatrixUL lhs, LorentzMatrixUU rhs)
        {
            return new LorentzMatrixUU(MatrixUtilities.MatrixMultiply((i, j) => lhs[i, j], (i, j) => rhs[i, j], lhs.Size, lhs.Operations));
        }
    }
}
