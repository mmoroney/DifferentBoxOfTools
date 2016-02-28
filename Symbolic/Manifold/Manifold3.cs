using Symbolic.Matrix;
using Symbolic.Matrix.Matrix3;
using Symbolic.Tensor;
using Symbolic.Utilities;
using Symbolic.Vector;
using Symbolic.Vector.Euclidean;
using Symbolic.Vector.Vector3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Manifold
{
    public class Manifold3 : ManifoldBase<Matrix3LL, Matrix3UU, Vector3OperatorL, Vector3OperatorU, Tensor4D3>
    {
        public Manifold3(Matrix3LL metric, Vector3OperatorL del)
            : base(metric, del)
        {
        }

        protected override Tensor4D3 CreateRiemannTensor(Func<int, int, int, int, Symbol> initializer)
        {
            return new Tensor4D3(initializer);
        }

        protected override Matrix3LL CreateRicciTensor(Func<int, int, Symbol> initializer)
        {
            return new Matrix3LL(initializer);
        }

        protected override Matrix3UU CreateContravariantMetric(Func<int, int, Symbol> initializer)
        {
            return new Matrix3UU(initializer);
        }
    }
}
