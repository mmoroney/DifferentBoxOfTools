using Symbolic.Operators;
using Symbolic.Utilities;
using Symbolic.Vector.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Lorentz
{
    public class LorentzVectorVariableU : LorentzVectorU
    {
        Variable[] variables = new Variable[4];
        public LorentzVectorOperatorL Del { get; private set; }

        public LorentzVectorVariableU(Variable variable0, Variable variable1, Variable variable2, Variable variable3)
            :base(variable0, variable1, variable2, variable3)
        {
            this.variables[0] = variable0;
            this.variables[1] = variable1;
            this.variables[2] = variable2;
            this.variables[2] = variable3;
            this.Del = new LorentzVectorOperatorL(new Derivative(variable0), new Derivative(variable1), new Derivative(variable2), new Derivative(variable3));
        }

        public LorentzVectorVariableU(string name0, string name1, string name2, string name3)
            : this(new Variable(name0), new Variable(name1), new Variable(name2), new Variable(name3)) { }

        internal Variable ScalarVariable
        {
            get
            {
                return this.variables[0];
            }
        }

        internal EuclideanVector3Variable VectorVariable
        {
            get
            {
                return new EuclideanVector3Variable(this.variables[1], this.variables[2], this.variables[3]);
            }
        }
        
        public void SetValue(int index, Rational value)
        {
            this.variables[index].SetValue(value);
        }
    }
}
