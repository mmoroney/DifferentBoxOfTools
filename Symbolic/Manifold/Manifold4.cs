using Symbolic.Matrix;
using Symbolic.Matrix.Matrix4;
using Symbolic.Tensor;
using Symbolic.Utilities;
using Symbolic.Vector;
using Symbolic.Vector.Euclidean;
using Symbolic.Vector.Vector4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Manifold
{
    public class Manifold4 : ManifoldBase<Matrix4LL, Matrix4UU, Vector4OperatorL, Vector4OperatorU, Tensor4D4>
    {
        public Manifold4(Matrix4LL metric, Vector4OperatorL del)
            : base(metric, del)
        {
        }

        protected override Tensor4D4 CreateRiemannTensor(Func<int, int, int, int, Symbol> initializer)
        {
            return new Tensor4D4(initializer);
        }

        protected override Matrix4LL CreateRicciTensor(Func<int, int, Symbol> initializer)
        {
            return new Matrix4LL(initializer);
        }

        protected override Matrix4UU CreateContravariantMetric(Func<int, int, Symbol> initializer)
        {
            return new Matrix4UU(initializer);
        }
    }
}
