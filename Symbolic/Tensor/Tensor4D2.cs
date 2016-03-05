using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Tensor
{
    public class Tensor4D2 : Tensor4DBase<Symbol>
    {
        public Tensor4D2(Func<int, int, int, int, Symbol> initializer) :
            base(2, initializer) { }

        public static Tensor4D2 operator +(Tensor4D2 lhs, Tensor4D2 rhs)
        {
            return new Tensor4D2((i, j, k, l) => lhs[i, j, k, l] + rhs[i, j, k, l]);
        }

        public static Tensor4D2 operator -(Tensor4D2 lhs, Tensor4D2 rhs)
        {
            return new Tensor4D2((i, j, k, l) => lhs[i, j, k, l] - rhs[i, j, k, l]);
        }

        public static Tensor4D2 operator -(Tensor4D2 a)
        {
            return new Tensor4D2((i, j, k, l) => -a[i, j, k, l]);
        }
    }
}
