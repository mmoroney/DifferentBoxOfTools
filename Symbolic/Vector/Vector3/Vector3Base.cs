using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector3
{
    public abstract class Vector3Base<TScalar, TVector, TInvert> : MetricVectorBase<TScalar, TVector, TInvert>
        where TVector : VectorBase<TScalar, TVector, TInvert>
        where TInvert : VectorBase<TScalar, TInvert, TVector>
    {
        internal Vector3Base(IOperations<TScalar> operations, Func<int, TScalar> initializer) : base(operations, 3, initializer) { }
    }
}
