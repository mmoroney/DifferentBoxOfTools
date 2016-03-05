using Symbolic.Operators;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector4
{
    public class Vector4OperatorU : Vector4Base<Operator, Vector4OperatorU, Vector4OperatorL>
    {
        public Vector4OperatorU(Operator operator0, Operator operator1, Operator operator2, Operator operator3)
            : this(VectorUtilities.Initializer(operator0, operator1, operator2, operator3)) { }

        public Vector4OperatorU(Func<int, Operator> initializer) : base(Operator.Operations, initializer) { }

        protected override Vector4OperatorU Create(Func<int, Operator> initializer)
        {
            return new Vector4OperatorU(initializer);
        }

    }
}
