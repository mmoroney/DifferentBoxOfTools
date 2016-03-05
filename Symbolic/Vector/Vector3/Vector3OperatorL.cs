using Symbolic.Operators;
using Symbolic.Utilities;
using Symbolic.Vector.Vector3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector3
{
    public class Vector3OperatorL : Vector3Base<Operator, Vector3OperatorL, Vector3OperatorU>
    {
        public Vector3OperatorL(Operator operator0, Operator operator1, Operator operator2)
            : this(VectorUtilities.Initializer(operator0, operator1, operator2)) { }

        public Vector3OperatorL(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        protected override Vector3OperatorL Create(Func<int, Operator> initializer)
        {
            return new Vector3OperatorL(initializer);
        }
    }
}
