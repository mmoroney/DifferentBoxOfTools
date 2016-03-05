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
    public class LorentzVectorOperatorU : LorentzVectorBase<Operator, LorentzVectorOperatorU, EuclideanVector3Operator, LorentzVectorOperatorL>
    {
        public static LorentzVectorOperatorU Zero { get; private set; }

        static LorentzVectorOperatorU()
        {
            LorentzVectorOperatorU.Zero = new LorentzVectorOperatorU(i => Operator.Zero);
        }
        
        public LorentzVectorOperatorU(Operator scalar, EuclideanVector3Operator vector)
            : this(scalar, vector[0], vector[1], vector[2]) { }
        public LorentzVectorOperatorU(Operator operator0, Operator operator1, Operator operator2, Operator operator3)
            : this(VectorUtilities.Initializer(operator0, operator1, operator2, operator3)) { }
        public LorentzVectorOperatorU(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        public LorentzVectorOperatorU Transform(EuclideanVector3 velocity)
        {
            return LorentzTransform.Matrix(velocity) * this;
        }

        protected override LorentzVectorOperatorU Create(Func<int, Operator> initializer)
        {
            return new LorentzVectorOperatorU(initializer);
        }

        protected override EuclideanVector3Operator CreateVector3(Func<int, Operator> initializer)
        {
            return new EuclideanVector3Operator(initializer);
        }

        protected override LorentzVectorOperatorL CreateInvert(Func<int, Operator> initializer)
        {
            return new LorentzVectorOperatorL(initializer);
        }
        
        public Symbol Dot(LorentzVectorL vector)
        {
            return this.Vector.Dot(vector.Vector) - this.Scalar * vector.Scalar;
        }
    }
}
