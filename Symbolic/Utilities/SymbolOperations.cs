using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Utilities
{
    internal class SymbolOperations : IOperations<Symbol>
    {
        public Symbol Zero
        {
            get { return Symbol.Zero; }
        }

        public Symbol One
        {
            get { return Symbol.One; }
        }

        public Symbol Add(Symbol lhs, Symbol rhs)
        {
            return lhs + rhs;
        }

        public Symbol Subtract(Symbol lhs, Symbol rhs)
        {
            return lhs - rhs;
        }

        public Symbol Negative(Symbol value)
        {
            return -value;
        }

        public Symbol Multiply(Symbol lhs, Symbol rhs)
        {
            return lhs * rhs;
        }

        public bool Compare(Symbol lhs, Symbol rhs)
        {
            return lhs == rhs;
        }

        public string GetCanonicalString(Symbol value)
        {
            return value.CanonicalString;
        }
    }
}
