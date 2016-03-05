using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Euclidean
{
    public abstract class EuclideanVector3Base<T, U> : EuclideanVectorBase<T, U>
        where U : EuclideanVector3Base<T, U>
    {
        internal EuclideanVector3Base(IOperations<T> operations, Func<int, T> initializer)
            : base(operations, 3, initializer) { }

        public U Cross(U rhs)
        {
            return this.Create(VectorUtilities.CrossProduct(i => this[i], i => rhs[i], this.Operations));
        }
    }
}
