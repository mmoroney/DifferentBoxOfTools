using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbolic.Operators;
using Symbolic.Utilities;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector2Operator : EuclideanVector2Base<Operator, EuclideanVector2Operator>
    {
        public static EuclideanVector2Operator Zero { get; private set; }

        static EuclideanVector2Operator()
        {
            EuclideanVector2Operator.Zero = new EuclideanVector2Operator(i => Operator.Zero);
        }
        
        public EuclideanVector2Operator(Operator operator0, Operator operator1) : this(VectorUtilities.Initializer(operator0, operator1)) { }
        public EuclideanVector2Operator(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        protected override EuclideanVector2Operator Create(Func<int, Operator> initializer)
        {
            return new EuclideanVector2Operator(initializer);
        }

        public Symbol Dot(EuclideanVector2 vector)
        {
            return VectorUtilities.ScalarProduct(i => this[i], i => vector[i], this.Size);
        }

        public static EuclideanVector2 operator *(EuclideanVector2Operator lhs, Symbol rhs)
        {
            return new EuclideanVector2(i => lhs[i] * rhs);
        }
    }
}
