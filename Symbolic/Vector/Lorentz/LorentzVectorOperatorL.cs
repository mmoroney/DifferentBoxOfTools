using Symbolic.Matrix;
using Symbolic.Matrix.Lorentz;
using Symbolic.Operators;
using Symbolic.Utilities;
using Symbolic.Vector.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Lorentz
{
    public class LorentzVectorOperatorL : LorentzVectorBase<Operator, LorentzVectorOperatorL, EuclideanVector3Operator, LorentzVectorOperatorU>
    {
        public static LorentzVectorOperatorL Zero { get; private set; }

        static LorentzVectorOperatorL()
        {
            LorentzVectorOperatorL.Zero = new LorentzVectorOperatorL(i => Operator.Zero);
        }
        
        public LorentzVectorOperatorL(Operator scalar, EuclideanVector3Operator vector)
            : this(scalar, vector[0], vector[1], vector[2]) { }
        public LorentzVectorOperatorL(Operator operator0, Operator operator1, Operator operator2, Operator operator3)
            : this(VectorUtilities.Initializer(operator0, operator1, operator2, operator3)) { }
        public LorentzVectorOperatorL(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        public LorentzVectorOperatorL Transform(EuclideanVector3 velocity)
        {
            return LorentzTransform.Matrix(velocity).Invert() * this;
        }

        protected override LorentzVectorOperatorL Create(Func<int, Operator> initializer)
        {
            return new LorentzVectorOperatorL(initializer);
        }

        protected override EuclideanVector3Operator CreateVector3(Func<int, Operator> initializer)
        {
            return new EuclideanVector3Operator(initializer);
        }

        protected override LorentzVectorOperatorU CreateInvert(Func<int, Operator> initializer)
        {
            return new LorentzVectorOperatorU(initializer);
        }

        public Symbol Dot(LorentzVectorU vector)
        {
            return this.Vector.Dot(vector.Vector) - this.Scalar * vector.Scalar;
        }
    }
}
