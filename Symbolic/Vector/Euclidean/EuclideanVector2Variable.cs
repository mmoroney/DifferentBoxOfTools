using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector2Variable : EuclideanVector2
    {
        Variable[] variables = new Variable[2];
        public EuclideanVector2Operator Del { get; private set; }

        public EuclideanVector2Variable(Variable variable0, Variable variable1)
            : base(variable0, variable1)
        {
            this.variables[0] = variable0;
            this.variables[1] = variable1;
            this.Del = new EuclideanVector2Operator(new Derivative(variable0), new Derivative(variable1));
        }

        public EuclideanVector2Variable(string name0, string name1)
            : this(new Variable(name0), new Variable(name1)) { }

        public void SetValue(int index, Rational value)
        {
            this.variables[index].SetValue(value);
        }
    }
}
