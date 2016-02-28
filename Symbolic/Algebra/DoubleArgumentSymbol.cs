using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Algebra
{
    internal abstract class DoubleArgumentSymbol : Symbol
    {
        protected Symbol Argument1 { get; private set; }
        protected Symbol Argument2 { get; private set; }

        public DoubleArgumentSymbol(Symbol argument1, Symbol argument2)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
        }
    }
}
