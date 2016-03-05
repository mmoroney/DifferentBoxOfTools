using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Tensor
{
    public class Tensor4D3 : Tensor4DBase<Symbol>
    {
        public Tensor4D3(Func<int, int, int, int, Symbol> initializer) :
            base(3, initializer) { }

        public static Tensor4D3 operator +(Tensor4D3 lhs, Tensor4D3 rhs)
        {
            return new Tensor4D3((i, j, k, l) => lhs[i, j, k, l] + rhs[i, j, k, l]);
        }

        public static Tensor4D3 operator -(Tensor4D3 lhs, Tensor4D3 rhs)
        {
            return new Tensor4D3((i, j, k, l) => lhs[i, j, k, l] - rhs[i, j, k, l]);
        }

        public static Tensor4D3 operator -(Tensor4D3 a)
        {
            return new Tensor4D3((i, j, k, l) => -a[i, j, k, l]);
        }
    }
}
