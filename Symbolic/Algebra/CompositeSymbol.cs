using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Algebra
{
    internal abstract class CompositeSymbol : Symbol
    {
        private static IComparer<Symbol> comparer = new CanonicalStringComparer();
        protected List<Symbol> Symbols { get; private set; }
        protected Rational ConstantTerm { get; private set; }

        public CompositeSymbol(List<Symbol> symbols, Rational constantTerm)
        {
            this.Symbols = symbols;
            this.ConstantTerm = constantTerm;
        }

        protected List<Symbol> GetSortedTerms()
        {
            List<Symbol> list = new List<Symbol>();

            for (int i = 0; i < this.Symbols.Count; i++)
            {
                list.Add(this.Symbols[i]);
            }

            list.Sort(comparer);

            return list;
        }

        private class CanonicalStringComparer : IComparer<Symbol>
        {
            public int Compare(Symbol x, Symbol y)
            {
                return x.CanonicalString.CompareTo(y.CanonicalString);
            }
        }
    }
}
