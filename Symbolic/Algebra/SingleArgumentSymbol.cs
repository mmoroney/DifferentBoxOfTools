using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Algebra
{
    internal abstract class SingleArgumentSymbol : Symbol
    {
        protected Symbol Argument { get; private set; }

        public SingleArgumentSymbol(Symbol argument)
        {
            this.Argument = argument;
        }
    }
}
