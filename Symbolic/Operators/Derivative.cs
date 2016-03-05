using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Operators
{
    public class Derivative : Operator
    {
        private Variable variable;

        public Derivative(Variable variable)
        {
            this.variable = variable;
        }

        protected override Symbol Operate(Symbol symbol)
        {
            return symbol.Diff(this.variable);
        }

        public override string ToString()
        {
            return string.Format("d/d{0}", this.variable);
        }
    }
}
