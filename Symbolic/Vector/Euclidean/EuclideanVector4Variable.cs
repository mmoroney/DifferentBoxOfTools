using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector4Variable : EuclideanVector4
    {
        Variable[] variables = new Variable[4];
        public EuclideanVector4Operator Del { get; private set; }

        public EuclideanVector4Variable(Variable variable0, Variable variable1, Variable variable2, Variable variable3)
            :base(variable0, variable1, variable2, variable3)
        {
            this.variables[0] = variable0;
            this.variables[1] = variable1;
            this.variables[2] = variable2;
            this.variables[3] = variable3;
            this.Del = new EuclideanVector4Operator(new Derivative(variable0), new Derivative(variable1), new Derivative(variable2), new Derivative(variable3));
        }

        public EuclideanVector4Variable(string name0, string name1, string name2, string name3)
            : this(new Variable(name0), new Variable(name1), new Variable(name2), new Variable(name3)) { }

        public void SetValue(int index, Rational value)
        {
            this.variables[index].SetValue(value);
        }
    }
}
