using Symbolic.Operators;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector2
{
    public class Vector2OperatorU : Vector2Base<Operator, Vector2OperatorU, Vector2OperatorL>
    {
        public Vector2OperatorU(Operator operator0, Operator operator1)
            : this(VectorUtilities.Initializer(operator0, operator1)) { }

        public Vector2OperatorU(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        protected override Vector2OperatorU Create(Func<int, Operator> initializer)
        {
            return new Vector2OperatorU(initializer);
        }
    }
}
