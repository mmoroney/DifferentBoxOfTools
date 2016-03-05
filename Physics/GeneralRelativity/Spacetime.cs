using Symbolic.Manifold;
using Symbolic.Matrix.Matrix4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.GeneralRelativity
{
    public class Spacetime
    {
        public Manifold4 Manifold { get; private set; }
        public Matrix4UU EinsteinTensor { get; private set; }
        public Matrix4UU EnergyMomentumTensor { get; private set; }

        public Spacetime(Manifold4 manifold)
        {
            this.Manifold = manifold;
            Matrix4UU covariantRicciTensor = manifold.ContravariantMetric * (manifold.RicciTensor * manifold.ContravariantMetric);
            this.EinsteinTensor = covariantRicciTensor - manifold.RicciScalar * manifold.ContravariantMetric / 2;
            this.EnergyMomentumTensor = this.EinsteinTensor;
        }
    }
}
