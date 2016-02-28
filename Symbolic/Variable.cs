using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic {
    public class Variable : Symbol {
        Rational value;
        string displayName;
        int id;
        static int nextId;
        public Operator Derivative { get; private set; }

        public Variable(string name, Rational value)
        {
            this.id = nextId++;
            this.displayName = name;
            this.value = value;
            this.Derivative = new Derivative(this);
        }

        public Variable() : this(null) { }
        public Variable(string name) : this(name, Rational.Zero) { }

        protected override string BuildCanonicalString()
        {
            return string.Format("x{0}", this.id);
        }

        protected override string BuildDisplayString()
        {
            if (string.IsNullOrEmpty(this.displayName))
                return BuildCanonicalString();

            return this.displayName;
        }

        public override Symbol Diff(Variable variable)
        {
            if (variable == this)
            {
                return Symbol.One;
            }
            return Symbol.Zero;
        }

        public override Rational Value
        {
            get { return this.value; }
        }

        public void SetValue(Rational value)
        {
            this.value = value;
        }
    }
}
