using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector2
{
    public abstract class Vector2Base<TScalar, TVector, TInvert> : MetricVectorBase<TScalar, TVector, TInvert>
        where TVector : VectorBase<TScalar, TVector, TInvert>
        where TInvert : VectorBase<TScalar, TInvert, TVector>
    {
        internal Vector2Base(IOperations<TScalar> operations, Func<int, TScalar> initializer) : base(operations, 2, initializer) { }
    }
}
