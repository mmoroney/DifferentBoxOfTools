using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Algebra
{
    internal class Sum : CompositeSymbol
    {
        public Sum(List<Symbol> symbols, Rational constantTerm) : base(symbols, constantTerm) { }

        public override Rational Value
        {
            get
            {
                Rational value = this.ConstantTerm;
                foreach (Symbol symbol in this.Symbols)
                {
                    value = value + symbol.Value;
                }

                return value;
            }
        }

        internal override string ToStringWithBrackets()
        {
            string toString = this.ToString();

            if (this.Symbols.Count == 1 && this.ConstantTerm == Rational.Zero)
            {
                return toString;
            }

            return string.Format("({0})", toString);
        }

        public override Symbol Diff(Variable variable)
        {
            Symbol diff = Symbol.Zero;
            foreach (Symbol current in this.Symbols)
            {
                diff = diff + current.Diff(variable);
            }

            return diff;
        }

        internal override List<Symbol> SumTerms
        {
            get
            {
                return this.Symbols;
            }
        }

        internal override Rational SumConstantTerm
        {
            get
            {
                return this.ConstantTerm;
            }
        }

        internal override Symbol Negative()
        {
            List<Symbol> symbols = new List<Symbol>();
            foreach (Symbol symbol in this.Symbols)
            {
                symbols.Add(symbol.Negative());
            }
            return new Sum(symbols, -this.ConstantTerm);
        }

        protected override Symbol OnAdd(Rational value)
        {
            if (this.Symbols.Count == 1 && value == -this.ConstantTerm)
            {
                return this.Symbols[0];
            }

            return new Sum(this.Symbols, this.ConstantTerm + value);
        }

        protected override string BuildDisplayString()
        {
            return BuildString(this.Symbols, s => s.ToString());
        }

        protected override string BuildCanonicalString()
        {
            return BuildString(this.GetSortedTerms(), s => s.CanonicalString);
        }

        private string BuildString(List<Symbol> items, Func<Symbol, string> getString)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Symbol symbol in items)
            {
                if (symbol.ProductConstantTerm.Numerator > 0)
                {
                    builder.Append("+");
                }

                builder.Append(getString(symbol));
            }

            if (this.ConstantTerm != Rational.Zero)
            {
                if (this.ConstantTerm.Numerator > 0)
                {
                    builder.Append('+');
                }

                builder.Append(this.ConstantTerm.ToString());
            }

            string ret = builder.ToString();

            if (ret[0] == '+')
            {
                ret = ret.Remove(0, 1);
            }

            return ret;
        }
    }
}
