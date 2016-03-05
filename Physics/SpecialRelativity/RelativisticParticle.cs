using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbolic;
using Symbolic.Vector;
using Symbolic.Operators;
using Symbolic.Vector.Lorentz;
using Symbolic.Vector.Euclidean;
using Symbolic.Matrix.Lorentz;

namespace Physics.SpecialRelativity
{
    public class RelativisticParticle
    {
        public Symbol Mass { get; private set; }
        public LorentzVectorU Position { get; private set; }
        public Operator InvariantTimeDerivative { get; private set; }
        public Symbol Gamma { get; private set; }
        public LorentzVectorU Velocity { get; private set; }

        public LorentzVectorU Momentum
        {
            get
            {
                return this.Mass * this.Velocity;
            }
        }

        public LorentzVectorU Force 
        {
            get
            {
                return this.InvariantTimeDerivative * this.Momentum;
            }
        }

        public RelativisticParticle(Symbol mass, EuclideanVector3 position, Variable time)
        {
            this.Mass = mass;
            this.Position = new LorentzVectorU(time, position);
            Operator dt = new Derivative(time);
            EuclideanVector3 v = dt * position;
            this.Gamma = LorentzTransform.Gamma(v);
            this.Velocity = this.Gamma * new LorentzVectorU(Symbol.One, v);
            this.InvariantTimeDerivative = this.Gamma * dt;
        }

        private RelativisticParticle(Symbol mass, LorentzVectorU position, Operator invariantTimeDerivative)
        {
            this.Mass = mass;
            this.Position = position;
            this.InvariantTimeDerivative = invariantTimeDerivative;
            this.Velocity = invariantTimeDerivative * this.Position;
            this.Gamma = this.Velocity.Scalar;
        }

        public RelativisticParticle Transform(EuclideanVector3 velocity)
        {
            LorentzMatrixUL L = LorentzTransform.Matrix(velocity);
            return new RelativisticParticle(this.Mass, L * this.Position, this.InvariantTimeDerivative);
        }
    }
}
