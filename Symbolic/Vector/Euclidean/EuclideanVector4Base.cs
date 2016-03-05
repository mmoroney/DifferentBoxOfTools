using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Euclidean
{
    public abstract class EuclideanVector4Base<T, U> : EuclideanVectorBase<T, U>
        where U : EuclideanVector4Base<T, U>
    {
        internal EuclideanVector4Base(IOperations<T> operations, Func<int, T> initializer)
            : base(operations, 4, initializer) { }
    }
}
