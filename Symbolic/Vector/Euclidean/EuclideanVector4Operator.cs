using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbolic.Operators;
using Symbolic.Utilities;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector4Operator : EuclideanVector4Base<Operator, EuclideanVector4Operator>
    {
        public static EuclideanVector4Operator Zero { get; private set; }

        static EuclideanVector4Operator()
        {
            EuclideanVector4Operator.Zero = new EuclideanVector4Operator(i => Operator.Zero);
        }
        
        public EuclideanVector4Operator(Operator operator0, Operator operator1, Operator operator2, Operator operator3)
            : this(VectorUtilities.Initializer(operator0, operator1, operator2, operator3)) { }
        public EuclideanVector4Operator(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        protected override EuclideanVector4Operator Create(Func<int, Operator> initializer)
        {
            return new EuclideanVector4Operator(initializer);
        }

        public Symbol Dot(EuclideanVector4 vector)
        {
            return VectorUtilities.ScalarProduct(i => this[i], i => vector[i], this.Size);
        }

        public static EuclideanVector4 operator *(EuclideanVector4Operator lhs, Symbol rhs)
        {
            return new EuclideanVector4(i => lhs[i] * rhs);
        }
    }
}
