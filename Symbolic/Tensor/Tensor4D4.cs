using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Tensor
{
    public class Tensor4D4 : Tensor4DBase<Symbol>
    {
        public Tensor4D4(Func<int, int, int, int, Symbol> initializer) :
            base(4, initializer) { }

        public static Tensor4D4 operator +(Tensor4D4 lhs, Tensor4D4 rhs)
        {
            return new Tensor4D4((i, j, k, l) => lhs[i, j, k, l] + rhs[i, j, k, l]);
        }

        public static Tensor4D4 operator -(Tensor4D4 lhs, Tensor4D4 rhs)
        {
            return new Tensor4D4((i, j, k, l) => lhs[i, j, k, l] - rhs[i, j, k, l]);
        }

        public static Tensor4D4 operator -(Tensor4D4 a)
        {
            return new Tensor4D4((i, j, k, l) => -a[i, j, k, l]);
        }
    }
}
