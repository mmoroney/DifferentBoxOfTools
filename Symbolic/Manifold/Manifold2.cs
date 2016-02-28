using Symbolic.Matrix;
using Symbolic.Matrix.Matrix2;
using Symbolic.Tensor;
using Symbolic.Utilities;
using Symbolic.Vector;
using Symbolic.Vector.Euclidean;
using Symbolic.Vector.Vector2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Manifold
{
    public class Manifold2 : ManifoldBase<Matrix2LL, Matrix2UU, Vector2OperatorL, Vector2OperatorU, Tensor4D2>
    {
        public Manifold2(Matrix2LL metric, Vector2OperatorL del)
            : base(metric, del)
        {
        }

        protected override Tensor4D2 CreateRiemannTensor(Func<int, int, int, int, Symbol> initializer)
        {
            return new Tensor4D2(initializer);
        }

        protected override Matrix2LL CreateRicciTensor(Func<int, int, Symbol> initializer)
        {
            return new Matrix2LL(initializer);
        }

        protected override Matrix2UU CreateContravariantMetric(Func<int, int, Symbol> initializer)
        {
            return new Matrix2UU(initializer);
        }
    }
}
