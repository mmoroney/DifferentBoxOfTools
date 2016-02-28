using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbolic.Algebra;
using Symbolic.Utilities;

namespace Symbolic {
    public abstract class Symbol {
        public static Symbol Zero { get; private set; }
        public static Symbol One { get; private set; }
        internal static IOperations<Symbol> Operations { get; private set; }

        static Symbol()
        {
            Symbol.Zero = new Constant(0);
            Symbol.One = new Constant(1);
            Operations = new SymbolOperations();
        }

        private string displayString;
        private string canonicalString;

        private string DisplayString
        {
            get
            {
                if (string.IsNullOrEmpty(this.displayString))
                {
                    this.displayString = this.BuildDisplayString();
                }
                return this.displayString;
            }
        }

        internal string CanonicalString
        {
            get
            {
                if (string.IsNullOrEmpty(this.canonicalString))
                {
                    this.canonicalString = this.BuildCanonicalString();
                }
                return this.canonicalString;
            }
        }

        protected abstract string BuildDisplayString();
        protected abstract string BuildCanonicalString();

        public abstract Rational Value
        {
            get;
        }

        internal virtual List<Symbol> SumTerms
        {
            get
            {
                List<Symbol> symbols = new List<Symbol>();
                symbols.Add(this);
                return symbols;
            }
        }

        internal virtual Rational SumConstantTerm
        {
            get
            {
                return Rational.Zero;
            }
        }

        internal virtual List<Symbol> ProductTerms
        {
            get
            {
                List<Symbol> symbols = new List<Symbol>();
                symbols.Add(this);
                return symbols;
            }
        }

        internal virtual Symbol ProductBase
        {
            get
            {
                return this;
            }
        }

        internal virtual Rational ProductConstantTerm
        {
            get
            {
                return Rational.One;
            }
        }

        public abstract Symbol Diff(Variable variable);

        internal virtual Symbol ExponentBase
        {
            get
            {
                return this;
            }
        }

        internal virtual Symbol Exponent
        {
            get
            {
                return Symbol.One;
            }
        }

        internal virtual Symbol Reciprocal()
        {
            return new Power(this, -Symbol.One);
        }

        internal virtual Symbol Negative()
        {
            List<Symbol> symbols = new List<Symbol>();
            symbols.Add(this);
            return new Product(symbols, -Rational.One);
        }

        internal Symbol Add(Rational value)
        {
            if (value == Rational.Zero)
            {
                return this;
            }

            return this.OnAdd(value);
        }

        protected virtual Symbol OnAdd(Rational value)
        {
            List<Symbol> symbols = new List<Symbol>();
            symbols.Add(this);
            return new Sum(symbols, value);
        }

        internal Symbol Multiply(Rational value)
        {
            if (value == Rational.Zero)
            {
                return Symbol.Zero;
            }

            if (value == Rational.One)
            {
                return this;
            }

            if (value == -Rational.One)
            {
                return this.Negative();
            }

            return this.OnMultiply(value);
        }

        protected virtual Symbol OnMultiply(Rational value)
        {
            List<Symbol> symbols = new List<Symbol>();
            symbols.Add(this);
            return new Product(symbols, value);
        }

        public virtual Symbol RaiseToExponent(Symbol exponent)
        {
            if (this == Symbol.Zero || this == Symbol.One)
            {
                return this;
            }
            if (exponent == Symbol.Zero)
            {
                return Symbol.One;
            }

            if (exponent == Symbol.One)
            {
                return this;
            }

            return this.OnRaiseToExponent(exponent);
        }

        protected virtual Symbol OnRaiseToExponent(Symbol exponent)
        {
            return new Power(this, exponent);
        }

        internal Symbol Log()
        {
            if (this == Symbol.One)
            {
                return Symbol.Zero;
            }

            return OnLog();
        }

        protected virtual Symbol OnLog()
        {
            return new NaturalLogarithm(this);
        }

        public override int GetHashCode()
        {
            return this.CanonicalString.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Symbol rhs = obj as Symbol;
            if (rhs == null)
            {
                return false;
            }

            return this.CanonicalString == rhs.CanonicalString;
        }

        public static bool operator ==(Symbol lhs, Symbol rhs)
        {
            if (System.Object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Symbol lhs, Symbol rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return this.DisplayString;
        }

        internal virtual string ToStringWithBrackets()
        {
            return this.ToString();
        }

        public static Symbol operator +(Symbol lhs, Symbol rhs)
        {
            List<Symbol> sumTerms = new List<Symbol>();
            foreach (Symbol sumTerm in lhs.SumTerms)
            {
                sumTerms.Add(sumTerm);
            }

            foreach (Symbol sumTerm in rhs.SumTerms)
            {
                sumTerms.Add(sumTerm);
            }

            Rational constantTerm = lhs.SumConstantTerm + rhs.SumConstantTerm;

            if (sumTerms.Count == 0)
            {
                return new Constant(constantTerm);
            }
            else if (sumTerms.Count == 1)
            {
                return sumTerms[0].Add(constantTerm);
            }

            List<int> positions;
            Symbol commonFactor = GetCommonFactor(sumTerms, out positions);

            if (positions.Count == 1)
            {
                Rational rational = sumTerms[0].ProductConstantTerm;
                List<Symbol> factoredSumTerms = new List<Symbol>();
                for (int i = 1; i < sumTerms.Count; i++)
                {
                    rational = rational.GreatestCommonFactor(sumTerms[i].ProductConstantTerm);
                }
                for (int i = 0; i < sumTerms.Count; i++)
                {
                    factoredSumTerms.Add(sumTerms[i].Multiply(1 / rational));
                }
                return (new Sum(factoredSumTerms, constantTerm / rational)).Multiply(rational);
            }

            Symbol factoredSum = Symbol.Zero;
            Symbol unfactoredSum = Symbol.Zero;
            int positionIndex = 0;

            for (int i = 0; i < sumTerms.Count; i++)
            {
                if(positionIndex < positions.Count && i == positions[positionIndex])
                {
                    factoredSum = factoredSum + sumTerms[i] / commonFactor;
                    positionIndex++;
                }
                else
                {
                    unfactoredSum = unfactoredSum + sumTerms[i];
                }
            }

            return (commonFactor * factoredSum + unfactoredSum).Add(constantTerm);
        }

        private static Symbol GetCommonFactor(List<Symbol> sumTerms, out List<int> positions)
        {
            Dictionary<Symbol, FactorInfo> factorDictionary = new Dictionary<Symbol, FactorInfo>();
            for (int i = 0; i < sumTerms.Count; i++)
            {
                Symbol sumTerm = sumTerms[i];
                foreach (Symbol productTerm in sumTerm.ProductTerms)
                {
                    Symbol factor = null;
                    Rational exponent = Rational.One;
                    if (productTerm.Exponent.SumTerms.Count == 0)
                    {
                        factor = productTerm.ExponentBase;
                        exponent = productTerm.Exponent.SumConstantTerm;
                    }
                    else
                    {
                        factor = productTerm;
                    }

                    FactorInfo factorInfo = null;
                    if (factorDictionary.TryGetValue(factor, out factorInfo))
                    {
                        factorInfo.Exponent = Rational.Min(factorInfo.Exponent, exponent);
                    }
                    else
                    {
                        factorInfo = new FactorInfo(factor, exponent);
                        factorDictionary[factor] = factorInfo;
                    }
                    factorInfo.Positions.Add(i);
                }
            }

            FactorInfo commonFactorInfo = null;

            foreach (KeyValuePair<Symbol, FactorInfo> pair in factorDictionary)
            {
                if (commonFactorInfo == null || pair.Value.Positions.Count > commonFactorInfo.Positions.Count)
                {
                    commonFactorInfo = pair.Value;
                }
                else if (commonFactorInfo.Positions.Count == pair.Value.Positions.Count)
                {
                    bool equal = true;
                    for (int i = 0; i < commonFactorInfo.Positions.Count; i++)
                    {
                        if (!(commonFactorInfo.Positions[i] == pair.Value.Positions[i]))
                        {
                            equal = false;
                            break;
                        }
                    }
                    if (equal)
                    {
                        commonFactorInfo = new FactorInfo(commonFactorInfo.GetCommonFactor() * pair.Value.GetCommonFactor(), Rational.One);
                        for (int i = 0; i < pair.Value.Positions.Count; i++)
                        {
                            commonFactorInfo.Positions.Add(pair.Value.Positions[i]);
                        }
                    }
                }
            }

            positions = commonFactorInfo.Positions;
            return commonFactorInfo.GetCommonFactor();
        }

        public static Symbol operator +(Symbol lhs, int rhs)
        {
            return lhs.Add(new Rational(rhs));
        }

        public static Symbol operator +(int lhs, Symbol rhs)
        {
            return rhs + lhs;
        }

        public static Symbol operator -(Symbol lhs, Symbol rhs)
        {
            return lhs + (-rhs);
        }

        public static Symbol operator -(Symbol lhs, int rhs)
        {
            return lhs + (-rhs);
        }

        public static Symbol operator -(int lhs, Symbol rhs)
        {
            return lhs + (-rhs);
        }

        public static Symbol operator *(Symbol lhs, Symbol rhs)
        {
            Rational constantTerm = lhs.ProductConstantTerm * rhs.ProductConstantTerm;

            if (constantTerm == Rational.Zero)
            {
                return Symbol.Zero;
            }

            List<Symbol> symbols = new List<Symbol>();
            foreach (Symbol symbol in lhs.ProductTerms)
            {
                symbols.Add(symbol);
            }

            foreach (Symbol symbol in rhs.ProductTerms)
            {
                int i = 0;
                while (i < symbols.Count)
                {
                    Symbol current = symbols[i];
                    if (current.ExponentBase == symbol.ExponentBase)
                    {
                        break;
                    }
                    else if (current.ExponentBase == -symbol.ExponentBase)
                    {
                        constantTerm = -constantTerm;
                        break;
                    }
                    i++;
                }

                if (i == symbols.Count)
                {
                    symbols.Add(symbol);
                }
                else if (symbols[i].Exponent == -symbol.Exponent)
                {
                    symbols.RemoveAt(i);
                }
                else
                {
                    symbols[i] = symbols[i].RaiseToExponent((symbols[i].Exponent + symbol.Exponent) / symbols[i].Exponent);
                }
            }

            if (symbols.Count == 0)
            {
                return new Constant(constantTerm);
            }
            else if (symbols.Count == 1 && constantTerm == Rational.One)
            {
                return symbols[0];
            }

            return new Product(symbols, constantTerm);
        }

        public static Symbol operator *(Symbol lhs, int rhs)
        {
            return lhs.Multiply(new Rational(rhs));
        }

        public static Symbol operator *(int lhs, Symbol rhs)
        {
            return rhs * lhs;
        }

        public static Symbol operator /(Symbol lhs, Symbol rhs)
        {
            return lhs * rhs.Reciprocal();
        }

        public static Symbol operator /(Symbol lhs, int rhs)
        {
            return lhs.Multiply(new Rational(1, rhs));
        }

        public static Symbol operator /(int lhs, Symbol rhs)
        {
            return rhs.Reciprocal().Multiply(new Rational(lhs));
        }

        public static Symbol operator -(Symbol symbol)
        {
            return symbol.Negative();
        }

        private class FactorInfo
        {
            public Symbol Symbol { get; private set; }
            Rational exponent;
            List<int> positions = new List<int>();

            public FactorInfo(Symbol symbol, Rational exponent)
            {
                this.Symbol = symbol;
                this.exponent = exponent;
            }

            public Rational Exponent
            {
                get { return this.exponent; }
                set { this.exponent = value; }
            }

            public List<int> Positions
            {
                get { return this.positions; }
            }

            public Symbol GetCommonFactor()
            {
                return this.Symbol.RaiseToExponent(new Constant(this.exponent));
            }
        }
    }
}
