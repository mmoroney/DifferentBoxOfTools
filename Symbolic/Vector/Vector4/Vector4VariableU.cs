using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector4
{
    public class Vector4VariableU : Vector4U
    {
        Variable[] variables = new Variable[4];
        public Vector4OperatorL Del { get; private set; }

        public Vector4VariableU(Variable variable0, Variable variable1, Variable variable2, Variable variable3)
            : base(variable0, variable1, variable2, variable3)
        {
            this.variables[0] = variable0;
            this.variables[1] = variable1;
            this.variables[2] = variable2;
            this.variables[3] = variable3;
            this.Del = new Vector4OperatorL(new Derivative(variable0), new Derivative(variable1), new Derivative(variable2), new Derivative(variable3));
        }

        public Vector4VariableU(string name0, string name1, string name2, string name3)
            : this(new Variable(name0), new Variable(name1), new Variable(name2), new Variable(name3)) { }

        public void SetValue(int index, Rational value)
        {
            this.variables[index].SetValue(value);
        }
    }
}
