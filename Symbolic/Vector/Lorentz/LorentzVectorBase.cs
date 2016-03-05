using Symbolic.Utilities;
using Symbolic.Vector;
using Symbolic.Vector.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Lorentz
{
    public abstract class LorentzVectorBase<TScalar, TLorentzVector, TVector3, TInvert> : VectorBase<TScalar, TLorentzVector, TInvert>
        where TLorentzVector : LorentzVectorBase<TScalar, TLorentzVector, TVector3, TInvert>
        where TVector3 : EuclideanVector3Base<TScalar, TVector3>
        where TInvert : LorentzVectorBase<TScalar, TInvert, TVector3, TLorentzVector>
    {
        internal LorentzVectorBase(IOperations<TScalar> operations, Func<int, TScalar> initializer) : base(operations, 4, initializer) { }

        public TScalar Scalar
        {
            get
            {
                return this[0];
            }
        }

        public TVector3 Vector
        {
            get
            {
                return this.CreateVector3(i => this[i + 1]);
            }
        }

        public TScalar InvariantScalar
        {
            get
            {
                return this.Dot(this.Invert());
            }
        }

        public TInvert Invert()
        {
            return this.CreateInvert(i => i == 0 ? this.Operations.Negative(this[i]) : this[i]);
        }

        protected abstract TVector3 CreateVector3(Func<int, TScalar> initializer);
        protected abstract TInvert CreateInvert(Func<int, TScalar> initializer);
    }
}
