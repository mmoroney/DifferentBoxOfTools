using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Tensor
{
    public abstract class Tensor4DBase<T>
    {
        private T[, , ,] components;

        internal Tensor4DBase(int length, Func<int, int, int, int, T> initializer)
        {
            this.components = ArrayUtilities.Initialize(length, length, length, length, initializer);
        }

        public T this[int i, int j, int k, int l]
        {
            get { return this.components[i, j, k, l]; }
        }
    }
}
