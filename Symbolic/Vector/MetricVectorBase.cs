using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector
{
    public abstract class MetricVectorBase<TScalar, TVector, TInvert> : VectorBase<TScalar, TVector, TInvert>
        where TVector : VectorBase<TScalar, TVector, TInvert>
        where TInvert : VectorBase<TScalar, TInvert, TVector>
    {
        internal MetricVectorBase(IOperations<TScalar> operations, int size, Func<int, TScalar> initializer) : base(operations, size, initializer) { }
    }
}
