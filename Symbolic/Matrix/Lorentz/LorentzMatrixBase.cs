using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Lorentz
{
    public abstract class LorentzMatrixBase<TScalar, TMatrix, TInvert> : MatrixBase<TScalar, TMatrix, TInvert>
        where TMatrix : LorentzMatrixBase<TScalar, TMatrix, TInvert>
        where TInvert : LorentzMatrixBase<TScalar, TInvert, TMatrix>
    {
        internal LorentzMatrixBase(IOperations<TScalar> operations, Func<int, int, TScalar> initializer) : base(operations, 4, initializer) { }

        public TScalar InvariantScalar
        {
            get
            {
                return this.Dot(this.Invert());
            }
        }
        
        protected abstract TInvert CreateInvert(Func<int, int, TScalar> initializer);

        public TInvert Invert()
        {
            return this.CreateInvert((i, j) => (i == 0 || j == 0) && i != j ? this.Operations.Negative(this[i, j]) : this[i, j]);
        }
    }
}
