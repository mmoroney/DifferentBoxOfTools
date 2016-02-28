using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Algebra
{
    internal class NaturalLogarithm : SingleArgumentSymbol
    {
        public NaturalLogarithm(Symbol argument)
            : base(argument)
        {
        }

        public override Symbol Diff(Variable variable)
        {
            return this.Argument.Reciprocal() * this.Argument.Diff(variable); ;
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
            return string.Format("Log({0})", argument);
        }
    }
}
