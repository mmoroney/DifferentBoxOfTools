using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector2
{
    public class Vector2VariableU : Vector2U
    {
        Variable[] variables = new Variable[4];
        public Vector2OperatorL Del { get; private set; }

        public Vector2VariableU(Variable variable0, Variable variable1)
            : base(variable0, variable1)
        {
            this.variables[0] = variable0;
            this.variables[1] = variable1;
            this.Del = new Vector2OperatorL(new Derivative(variable0), new Derivative(variable1));
        }

        public Vector2VariableU(string name0, string name1)
            : this(new Variable(name0), new Variable(name1)) { }

        public void SetValue(int index, Rational value)
        {
            this.variables[index].SetValue(value);
        }
    }
}
