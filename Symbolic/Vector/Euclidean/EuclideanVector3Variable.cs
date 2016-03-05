using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector3Variable : EuclideanVector3
    {
        Variable[] variables = new Variable[3];
        public EuclideanVector3Operator Del { get; private set; }

        public EuclideanVector3Variable(Variable variable0, Variable variable1, Variable variable2)
            :base(variable0, variable1, variable2)
        {
            this.variables[0] = variable0;
            this.variables[1] = variable1;
            this.variables[2] = variable2;
            this.Del = new EuclideanVector3Operator(new Derivative(variable0), new Derivative(variable1), new Derivative(variable2));
        }

        public EuclideanVector3Variable(string name0, string name1, string name2)
            : this(new Variable(name0), new Variable(name1), new Variable(name2)) { }

        public void SetValue(int index, Rational value)
        {
            this.variables[index].SetValue(value);
        }
    }
}
