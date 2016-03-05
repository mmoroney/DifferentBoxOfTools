using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Euclidean
{
    public abstract class EuclideanVectorBase<TScalar, TVector> : VectorBase<TScalar, TVector, TVector>
        where TVector : EuclideanVectorBase<TScalar, TVector>
    {
        internal EuclideanVectorBase(IOperations<TScalar> operations, int size, Func<int, TScalar> initializer)
            : base(operations, size, initializer) { }

        public TScalar InvariantScalar
        {
            get
            {
                return this.Dot(this);
            }
        }
    }
}
