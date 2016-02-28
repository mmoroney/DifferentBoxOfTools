using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Algebra
{
    internal class Constant : Symbol
    {
        Rational value;

        public Constant(Rational value)
        {
            this.value = value;
        }

        public Constant(int numerator = 0, int denominator = 1) : this(new Rational(numerator, denominator)) { }

        public override Rational Value
        {
            get { return this.value; }
        }

        protected override string BuildCanonicalString()
        {
            return string.Format("c{0}", this.value);
        }

        protected override string BuildDisplayString()
        {
            return this.value.ToString();
        }

        public override Symbol Diff(Variable variable)
        {
            return Symbol.Zero;
        }

        internal override List<Symbol> SumTerms
        {
            get
            {
                return new List<Symbol>();
            }
        }

        internal override Rational SumConstantTerm
        {
            get
            {
                return this.value;
            }
        }

        internal override List<Symbol> ProductTerms
        {
            get
            {
                return new List<Symbol>();
            }
        }

        internal override Rational ProductConstantTerm
        {
            get
            {
                return this.value;
            }
        }

        internal override Symbol Reciprocal()
        {
            return new Constant(this.value.Denominator, this.value.Numerator);
        }

        internal override Symbol Negative()
        {
            return new Constant(-this.value);
        }

        protected override Symbol OnAdd(Rational value)
        {
            return new Constant(this.value + value);
        }

        protected override Symbol OnMultiply(Rational coefficient)
        {
            return new Constant(this.value * coefficient);
        }
    }
}
