using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Euclidean
{
    public abstract class EuclideanVector2Base<T, U> : EuclideanVectorBase<T, U>
        where U : EuclideanVector2Base<T, U>
    {
        internal EuclideanVector2Base(IOperations<T> operations, Func<int, T> initializer)
            : base(operations, 2, initializer) { }
    }
}
