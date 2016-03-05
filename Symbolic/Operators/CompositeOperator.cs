using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Operators
{
    internal abstract class CompositeOperator : Operator
    {
        List<Operator> operators;

        public CompositeOperator(List<Operator> operators)
        {
            this.operators = operators;
        }

        public List<Operator> Operators
        {
            get
            {
                return this.operators;
            }
        }
    }
}
