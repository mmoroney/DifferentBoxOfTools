using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector3
{
    public class Vector3VariableU : Vector3U
    {
        Variable[] variables = new Variable[4];
        public Vector3OperatorL Del { get; private set; }

        public Vector3VariableU(Variable variable0, Variable variable1, Variable variable2)
            : base(variable0, variable1, variable2)
        {
            this.variables[0] = variable0;
            this.variables[1] = variable1;
            this.variables[2] = variable2;
            this.Del = new Vector3OperatorL(new Derivative(variable0), new Derivative(variable1), new Derivative(variable2));
        }

        public Vector3VariableU(string name0, string name1, string name2, string name3)
            : this(new Variable(name0), new Variable(name1), new Variable(name2)) { }

        public void SetValue(int index, Rational value)
        {
            this.variables[index].SetValue(value);
        }
    }
}
