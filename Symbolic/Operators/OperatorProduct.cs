using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Operators
{
    internal class OperatorProduct : CompositeOperator
    {
        public OperatorProduct(List<Operator> operators) : base(operators) { }

        protected override Symbol Operate(Symbol symbol)
        {
            for (int i = this.Operators.Count - 1; i >= 0; i--)
            {
                symbol = this.Operators[i] * symbol;
            }

            return symbol;
        }

        internal override List<Operator> SumTerms
        {
            get
            {
                if (this.Operators.Count == 2)
                {
                    if (this.Operators[0].ProductTerms.Count == 0)
                    {
                        List<Operator> operators = new List<Operator>();
                        foreach (Operator op in this.Operators[1].SumTerms)
                        {
                            operators.Add(op.Multiply(this.Operators[0].SymbolTerm, true));
                        }

                        return operators;
                    }
                    else if (this.Operators[1].ProductTerms.Count == 0)
                    {
                        List<Operator> operators = new List<Operator>();
                        foreach (Operator op in this.Operators[0].SumTerms)
                        {
                            operators.Add(op.Multiply(this.Operators[1].SymbolTerm, false));
                        }

                        return operators;
                    }
                }

                return base.SumTerms;
            }
        }

        internal override List<Operator> ProductTerms
        {
            get
            {
                return this.Operators;
            }
        }

        protected override Operator OnMultiply(Symbol symbol, bool front)
        {
            List<Operator> operators = new List<Operator>();

            foreach(Operator op in this.Operators) 
            {
                operators.Add(op);
            }

            int index = front ? 0 : operators.Count - 1;

            Operator target = this.Operators[(front) ? 0 : operators.Count - 1];

            if (target.SumTerms.Count == 0)
            {
                Symbol newSymbol = symbol * target.SymbolTerm;
                if (newSymbol == Symbol.One)
                {
                    this.Operators.RemoveAt(index);
                    if (this.Operators.Count == 1)
                    {
                        return this.Operators[0];
                    }
                }

                operators[index] = new OperatorSymbol(newSymbol);
            }
            else
            {
                Operator newOperatorSymbol = new OperatorSymbol(symbol);
                if (front)
                {
                    operators.Insert(0, newOperatorSymbol);
                }
                else
                {
                    operators.Add(newOperatorSymbol);
                }
            }

            return new OperatorProduct(operators);
        }

        internal override Operator RemoveOperator(bool front)
        {
            if (front)
            {
                if (this.Operators.Count == 2)
                {
                    return this.Operators[1];
                }
                else 
                {
                    List<Operator> operators = new List<Operator>();
                    for (int i = 1; i < this.Operators.Count; i++)
                    {
                        operators.Add(this.Operators[i]);
                    }
                    return new OperatorProduct(operators);
                }
            }
            else
            {
                if (this.Operators.Count == 2)
                {
                    return this.Operators[0];
                }
                else
                {
                    List<Operator> operators = new List<Operator>();
                    for (int i = 0; i < this.Operators.Count - 1; i++)
                    {
                        operators.Add(this.Operators[i]);
                    }
                    return new OperatorProduct(operators);
                }
            }
        }
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < this.Operators.Count; i++)
            {
                if (i == 0 && this.Operators[0].SymbolTerm == -Symbol.One)
                {
                    stringBuilder.Append('-');
                }
                else
                {
                    stringBuilder.Append(this.Operators[i].ToStringWithBrackets());
                }
            }

            return stringBuilder.ToString();
        }
    }
}
