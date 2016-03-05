using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector4
{
    public abstract class Vector4Base<TScalar, TVector, TInvert> : MetricVectorBase<TScalar, TVector, TInvert>
        where TVector : VectorBase<TScalar, TVector, TInvert>
        where TInvert : VectorBase<TScalar, TInvert, TVector>
    {
        internal Vector4Base(IOperations<TScalar> operations, Func<int, TScalar> initializer) : base(operations, 4, initializer) { }
    }
}
