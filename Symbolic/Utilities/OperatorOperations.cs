using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Utilities
{
    internal class OperatorOperations : IOperations<Operator>
    {
        public Operator Zero
        {
            get { return Operator.Zero; }
        }

        public Operator One
        {
            get { return Operator.One; }
        }

        public Operator Add(Operator lhs, Operator rhs)
        {
            return lhs + rhs;
        }

        public Operator Subtract(Operator lhs, Operator rhs)
        {
            return lhs - rhs;
        }

        public Operator Negative(Operator value)
        {
            return -value;
        }

        public Operator Multiply(Operator lhs, Operator rhs)
        {
            return lhs * rhs;
        }

        public bool Compare(Operator lhs, Operator rhs)
        {
            return lhs == rhs;
        }

        public string GetCanonicalString(Operator value)
        {
            return value.ToString();
        }
    }
}
