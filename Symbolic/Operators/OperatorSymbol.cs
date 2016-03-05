using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Operators
{
    internal class OperatorSymbol : Operator
    {
        private Symbol symbol;

        public OperatorSymbol(Symbol symbol)
        {
            this.symbol = symbol;
        }

        internal override bool IsSymbol
        {
            get
            {
                return true;
            }
        }

        protected override Symbol Operate(Symbol symbol)
        {
            return this.symbol * symbol;
        }

        internal override List<Operator> SumTerms
        {
            get
            {
                return new List<Operator>();
            }
        }

        internal override List<Operator> ProductTerms
        {
            get
            {
                return new List<Operator>();
            }
        }

        internal override Symbol SymbolTerm
        {
            get
            {
                return this.symbol;
            }
        }

        protected override Operator OnAdd(Symbol symbol)
        {
            return new OperatorSymbol(this.symbol + symbol);
        }

        protected override Operator OnAdd(Operator op)
        {
            Symbol newSymbol = this.SymbolTerm + op.SymbolTerm;
            if (newSymbol == Symbol.Zero && op.SumTerms.Count == 1)
            {
                return op.SumTerms[0];
            }

            return new OperatorSum(op.SumTerms, newSymbol);
        }
        
        protected override Operator OnMultiply(Symbol symbol, bool front)
        {
            return new OperatorSymbol(this.symbol * symbol);
        }

        protected override Operator OnMultiply(Operator op, bool front)
        {
            if (this == Operator.Zero)
            {
                return Operator.Zero;
            }
            if (this == Operator.One)
            {
                return op;
            }

            if (op.IsSymbol)
            {
                return new OperatorSymbol(this.SymbolTerm * op.SymbolTerm);
            }

            return base.OnMultiply(op, front);
        }
        
        public override Operator Negative()
        {
            return new OperatorSymbol(-this.symbol);
        }

        public override string ToString()
        {
            return this.symbol.ToString();
        }
    }
}
