using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Operators
{
    internal class OperatorSum : CompositeOperator
    {
        Symbol symbolTerm;
        public OperatorSum(List<Operator> operators, Symbol symbolTerm)
            : base(operators)
        {
            this.symbolTerm = symbolTerm;
        }

        protected override Symbol Operate(Symbol symbol)
        {
            Symbol result = this.symbolTerm;
            foreach (Operator op in this.Operators)
            {
                result = result + op * symbol;
            }
            return result;
        }

        internal override List<Operator> SumTerms
        {
            get
            {
                return this.Operators;
            }
        }

        protected override Operator OnAdd(Symbol symbol)
        {
            Symbol sumSymbolTerm = symbol + this.symbolTerm;
            if (this.Operators.Count == 1 && sumSymbolTerm == Symbol.Zero)
            {
                return this.Operators[0];
            }

            return new OperatorSum(this.Operators, sumSymbolTerm);
        }

        protected override Operator OnAdd(Operator op)
        {
            Symbol newSymbol = this.SymbolTerm + op.SymbolTerm;

            List<Operator> operators = new List<Operator>();
            foreach (Operator sumTerm in this.Operators)
            {
                operators.Add(sumTerm);
            }
            foreach (Operator sumTerm in op.SumTerms)
            {
                operators.Add(sumTerm);
            }
            return new OperatorSum(operators, newSymbol);
        }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Operator op in this.Operators)
            {
                string current = op.ToString();
                if (current[0] != '-')
                {
                    stringBuilder.Append('+');
                }

                stringBuilder.Append(current);
            }

            if (this.symbolTerm != Symbol.Zero)
            {
                string sumSymbolTermString = this.symbolTerm.ToString();
                if (sumSymbolTermString[0] != '-')
                {
                    stringBuilder.Append('+');
                }

                stringBuilder.Append(sumSymbolTermString);
            }

            string ret = stringBuilder.ToString();

            if (ret[0] == '+')
            {
                ret = ret.Remove(0, 1);
            }

            return ret;
        }

        internal override string ToStringWithBrackets()
        {
            return string.Format("({0})", this.ToString());
        }
    }
}
