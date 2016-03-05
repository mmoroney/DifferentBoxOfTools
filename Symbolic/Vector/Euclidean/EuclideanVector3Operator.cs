using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbolic.Operators;
using Symbolic.Utilities;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector3Operator : EuclideanVector3Base<Operator, EuclideanVector3Operator>
    {
        public static EuclideanVector3Operator Zero { get; private set; }

        static EuclideanVector3Operator()
        {
            EuclideanVector3Operator.Zero = new EuclideanVector3Operator(i => Operator.Zero);
        }

        public EuclideanVector3Operator(Operator operator0, Operator operator1, Operator operator2)
            : this(VectorUtilities.Initializer(operator0, operator1, operator2)) { }
        public EuclideanVector3Operator(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        protected override EuclideanVector3Operator Create(Func<int, Operator> initializer)
        {
            return new EuclideanVector3Operator(initializer);
        }

        public Symbol Dot(EuclideanVector3 vector)
        {
            return VectorUtilities.ScalarProduct(i => this[i], i => vector[i], this.Size);
        }

        public EuclideanVector3 Cross(EuclideanVector3 vector)
        {
            return new EuclideanVector3(VectorUtilities.CrossProduct(i => this[i], i => vector[i]));
        }

        public static EuclideanVector3 operator*(EuclideanVector3Operator lhs, Symbol rhs)
        {
            return new EuclideanVector3(i => lhs[i] * rhs);
        }
    }
}
