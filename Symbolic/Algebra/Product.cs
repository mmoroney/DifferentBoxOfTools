  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Algebra
{
    internal class Product : CompositeSymbol
    {
        public Product(List<Symbol> variables, Rational constantTerm) : base(variables, constantTerm) { }

        public override Rational Value
        {
            get
            {
                Rational value = this.ConstantTerm;
                foreach (Symbol symbol in this.Symbols)
                {
                    value = value * symbol.Value;
                }
                return value;
            }
        }

        internal override string ToStringWithBrackets()
        {
            string toString = this.ToString();
            if (this.Symbols.Count == 0)
                return toString;

            return string.Format("({0})", toString);
        }

        public override Symbol Diff(Variable variable)
        {
            Symbol numerator = Symbol.Zero;

            for (int i = 0; i < this.Symbols.Count; i++)
            {
                Symbol current = Symbol.One;
                for (int j = 0; j < this.Symbols.Count; j++)
                {
                    if (i == j)
                    {
                        current = current * this.Symbols[j].Diff(variable);
                    }
                    else
                    {
                        current = current * this.Symbols[j];
                    }
                }
                numerator = numerator + current;
            }

            return numerator.Multiply(this.ConstantTerm);
        }

        internal override List<Symbol> SumTerms
        {
            get
            {
                if (this.Symbols.Count == 1)
                {
                    List<Symbol> symbols = new List<Symbol>();
                    foreach (Symbol symbol in this.Symbols[0].SumTerms)
                    {
                        symbols.Add(symbol.Multiply(this.ConstantTerm));
                    }

                    return symbols;
                }
                return base.SumTerms;
            }
        }

        internal override Rational SumConstantTerm
        {
            get
            {
                if (this.Symbols.Count == 1)
                {
                    return Symbols[0].SumConstantTerm * this.ConstantTerm;
                }

                return base.SumConstantTerm;
            }
        }

        internal override List<Symbol> ProductTerms
        {
            get
            {
                return this.Symbols;
            }
        }

        internal override Rational ProductConstantTerm
        {
            get
            {
                return this.ConstantTerm;
            }
        }

        internal override Symbol ProductBase
        {
            get
            {
                if (this.Symbols.Count == 1)
                {
                    return this.Symbols[0];
                }
                return new Product(this.Symbols, Rational.One);
            }
        }

        protected override Symbol OnAdd(Rational value)
        {
            if (this.Symbols.Count == 1)
            {
                return this.Symbols[0].Add(value / this.ConstantTerm).Multiply(this.ConstantTerm);
            }

            return base.OnAdd(value);
        }

        protected override Symbol OnMultiply(Rational coefficient)
        {
            Rational newCoefficient = coefficient * this.ConstantTerm;

            if (newCoefficient == Rational.One && this.Symbols.Count == 1)
            {
                return this.Symbols[0];
            }

            return new Product(this.Symbols, newCoefficient);
        }

        internal override Symbol Reciprocal()
        {
            List<Symbol> symbols = new List<Symbol>();
            foreach (Symbol symbol in this.Symbols)
            {
                symbols.Add(symbol.Reciprocal());
            }

            return new Product(symbols, 1 / this.ConstantTerm);
        }

        internal override Symbol Negative()
        {
            return new Product(this.Symbols, -this.ConstantTerm);
        }

        protected override string BuildDisplayString()
        {
            return BuildString(this.Symbols, s => s.ToStringWithBrackets());
        }

        protected override string BuildCanonicalString()
        {
            return BuildString(this.GetSortedTerms(), s => s.CanonicalString);
        }
        
        private string BuildString(List<Symbol> items, Func<Symbol, string> getString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (this.ConstantTerm == -Rational.One)
            {
                stringBuilder.Append('-');
            }
            else if (this.ConstantTerm != Rational.One)
            {
                stringBuilder.Append(this.ConstantTerm);
            }

            bool firstSymbol = true;
            foreach (Symbol symbol in items)
            {
                if (firstSymbol)
                {
                    firstSymbol = false;
                }
                else
                {
                    stringBuilder.Append('*');
                }

                stringBuilder.Append(getString(symbol));
            }

            return stringBuilder.ToString();
        }
    }
}
