using Symbolic.Operators;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector2
{
    public class Vector2OperatorL : Vector2Base<Operator, Vector2OperatorL, Vector2OperatorU>
    {
        public Vector2OperatorL(Operator operator0, Operator operator1)
            : this(VectorUtilities.Initializer(operator0, operator1)) { }

        public Vector2OperatorL(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        protected override Vector2OperatorL Create(Func<int, Operator> initializer)
        {
            return new Vector2OperatorL(initializer);
        }
    }
}
