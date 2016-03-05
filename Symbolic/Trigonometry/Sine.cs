using Symbolic.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Trigonometry
{
    internal class Sine : SingleArgumentSymbol
    {
        public Sine(Symbol argument)
            : base(argument)
        {
        }

        public override Symbol Diff(Variable variable)
        {
            return new Cosine(this.Argument) * this.Argument.Diff(variable); ;
        }

        public override Rational Value
        {
            get { throw new NotSupportedException(); }
        }

        protected override string BuildDisplayString()
        {
            return BuildString(this.Argument.ToStringWithBrackets());
        }

        protected override string BuildCanonicalString()
        {
            return BuildString(this.Argument.CanonicalString);
        }

        private string BuildString(string argument)
        {
            return string.Format("sin({0})", argument);
        }
    }
}
