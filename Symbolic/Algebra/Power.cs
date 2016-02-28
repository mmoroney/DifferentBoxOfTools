using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Algebra
{
    internal class Power : DoubleArgumentSymbol
    {
        public Power(Symbol argument1, Symbol argument2) : base(argument1, argument2) { }

        public override Symbol Diff(Variable variable)
        {
            return this.Argument2 * Functions.Pow(this.Argument1, this.Argument2 - 1) * this.Argument1.Diff(variable); ;
        }

        public override Rational Value
        {
            get
            {
                throw new NotSupportedException(); 
            }
        }

        internal override List<Symbol> ProductTerms
        {
            get
            {
                List<Symbol> productTerms = this.Argument1.ProductTerms;
                if (productTerms.Count == 1)
                {
                    return base.ProductTerms;
                }

                List<Symbol> powers = new List<Symbol>();
                foreach (Symbol symbol in productTerms)
                {
                    powers.Add(symbol.RaiseToExponent(this.Argument2));
                }
                return powers;
            }
        }

        internal override Symbol Reciprocal()
        {
            if (this.Argument2 == -Symbol.One)
            {
                return this.Argument1;
            }
            return new Power(this.Argument1, -this.Argument2);
        }

        internal override Symbol Exponent
        {
            get
            {
                return this.Argument2;
            }
        }

        internal override Symbol ExponentBase
        {
            get
            {
                return this.Argument1;
            }
        }

        protected override Symbol OnRaiseToExponent(Symbol exponent)
        {
            return Functions.Pow(this.Argument1, this.Argument2 * exponent);
        }

        protected override Symbol OnLog()
        {
            return this.Exponent * this.ExponentBase.Log();
        }

        protected override string BuildDisplayString()
        {
            return BuildString(this.Argument1.ToStringWithBrackets(), this.Argument2.ToStringWithBrackets());
        }

        protected override string BuildCanonicalString()
        {
            return BuildString(this.Argument1.CanonicalString, this.Argument2.CanonicalString);
        }

        private string BuildString(string argument1, string argument2)
        {
            return string.Format("{0}^{1}", argument1, argument2);
        }
    }
}
