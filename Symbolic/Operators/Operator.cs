using Symbolic.Algebra;
using Symbolic.Utilities;
using Symbolic.Vector;
using Symbolic.Vector.Euclidean;
using Symbolic.Vector.Lorentz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Operators
{
    public abstract class Operator
    {
        public static Operator Zero { get; private set; }
        public static Operator One { get; private set; }
        internal static IOperations<Operator> Operations { get; private set; }

        static Operator()
        {
            Operator.Zero = new OperatorSymbol(Symbol.Zero);
            Operator.One = new OperatorSymbol(Symbol.One);
            Operator.Operations = new OperatorOperations();
        }
        
        protected abstract Symbol Operate(Symbol symbol);

        internal virtual bool IsSymbol
        {
            get
            {
                return false;
            }
        }

        internal virtual List<Operator> SumTerms
        {
            get
            {
                List<Operator> operators = new List<Operator>();
                operators.Add(this);
                return operators;
            }
        }

        internal virtual Symbol SymbolTerm
        {
            get
            {
                return Symbol.Zero;
            }
        }

        internal virtual List<Operator> ProductTerms
        {
            get
            {
                List<Operator> operators = new List<Operator>();
                operators.Add(this);
                return operators;
            }
        }

        internal Operator Add(Symbol symbol)
        {
            if (symbol == Symbol.Zero)
            {
                return this;
            }
            return this.OnAdd(symbol);
        }

        protected virtual Operator OnAdd(Symbol symbol)
        {
            List<Operator> operators = new List<Operator>();
            operators.Add(this);

            return new OperatorSum(operators, symbol);
        }

        internal Operator Add(Operator op)
        {
            if (op == Operator.Zero)
            {
                return this;
            }
            return this.OnAdd(op);
        }

        protected virtual Operator OnAdd(Operator op)
        {
            List<Operator> operators = new List<Operator>();
            operators.Add(this);
            foreach (Operator sumTerm in op.SumTerms)
            {
                operators.Add(sumTerm);
            }

            return new OperatorSum(operators, this.SymbolTerm + op.SymbolTerm);
        }

        internal Operator Multiply(Symbol symbol, bool front)
        {
            if (symbol == Symbol.Zero)
            {
                return Operator.Zero;
            }
            if (symbol == Symbol.One)
            {
                return this;
            }
            return this.OnMultiply(symbol, front);
        }

        protected virtual Operator OnMultiply(Symbol symbol, bool front)
        {
            List<Operator> operators = new List<Operator>();
            operators.Add(this);
            OperatorSymbol multiplier = new OperatorSymbol(symbol);
            operators.Insert(front ? 0 : 1, multiplier);
            return new OperatorProduct(operators);
        }

        internal Operator Multiply(Operator op, bool front)
        {
            if (op == Operator.Zero)
            {
                return Operator.Zero;
            }
            if (op == Operator.One)
            {
                return this;
            }
            return this.OnMultiply(op, front);
        }

        protected virtual Operator OnMultiply(Operator op, bool front)
        {
            List<Operator> operators = new List<Operator>();
            operators.Add(this);
            if (front)
            {
                operators.Insert(0, op);
            }
            else
            {
                operators.Add(op);
            }
            return new OperatorProduct(operators);
        }

        public virtual Operator Negative()
        {
            List<Operator> operators = new List<Operator>();
            operators.Add(new OperatorSymbol(-Symbol.One));
            operators.Add(this);
            return new OperatorProduct(operators);
        }

        internal virtual Operator RemoveOperator(bool front)
        {
            return Operator.One;
        }

        internal virtual string ToStringWithBrackets()
        {
            return this.ToString();
        }

        public static Operator operator+(Operator lhs, Operator rhs)
        {
            List<Operator> sumTerms = new List<Operator>();
            foreach (Operator sumTerm in lhs.SumTerms)
            {
                sumTerms.Add(sumTerm);
            }

            foreach (Operator sumTerm in rhs.SumTerms)
            {
                sumTerms.Add(sumTerm);
            }

            Symbol symbolTerm = lhs.SymbolTerm + rhs.SymbolTerm;

            if (sumTerms.Count == 0)
            {
                return new OperatorSymbol(symbolTerm);
            }
            else if (sumTerms.Count == 1)
            {
                return sumTerms[0].Add(symbolTerm);
            }

            List<int> positions;
            bool front;
            Operator commonFactor = GetCommonFactor(sumTerms, out positions, out front);

            if (positions.Count == 1)
            {
                return new OperatorSum(sumTerms, symbolTerm);
            }

            Operator factoredSum = Operator.Zero;
            Operator unfactoredSum = Operator.Zero;
            int positionIndex = 0;

            for (int i = 0; i < sumTerms.Count; i++)
            {
                if (positionIndex < positions.Count && i == positions[positionIndex])
                {
                    if (commonFactor.IsSymbol)
                    {
                        factoredSum = factoredSum + sumTerms[i].Multiply(commonFactor.SymbolTerm.Reciprocal(), front);
                    }
                    else
                    {
                        factoredSum = factoredSum + sumTerms[i].RemoveOperator(front);
                    }
                    positionIndex++;
                }
                else
                {
                    unfactoredSum = unfactoredSum + sumTerms[i];
                }
            }
            
            if (commonFactor.IsSymbol)
            {
                return factoredSum.Multiply(commonFactor.SymbolTerm, front).Add(unfactoredSum);
            }
            else
            {
                return factoredSum.Multiply(commonFactor, front).Add(unfactoredSum);
            }
        }

        private static Operator GetCommonFactor(List<Operator> sumTerms, out List<int> positions, out bool front)
        {
            Dictionary<Operator, FactorInfo> factorDictionaryFront = new Dictionary<Operator, FactorInfo>();
            Dictionary<Operator, FactorInfo> factorDictionaryBack = new Dictionary<Operator, FactorInfo>();

            for (int i = 0; i < sumTerms.Count; i++)
            {
                Operator sumTerm = sumTerms[i];
                UpdateFactorDictionaryWithOperator(factorDictionaryFront, sumTerm.ProductTerms[0], i);
                UpdateFactorDictionaryWithOperator(factorDictionaryBack, sumTerm.ProductTerms[sumTerm.ProductTerms.Count - 1], i);
            }

            FactorInfo commonFactorInfoFront = GetFactorInfo(factorDictionaryFront);
            FactorInfo commonFactorInfoBack = GetFactorInfo(factorDictionaryBack);

            FactorInfo commonFactorInfo;

            if (commonFactorInfoFront.Positions.Count >= commonFactorInfoBack.Positions.Count)
            {
                commonFactorInfo = commonFactorInfoFront;
                front = true;
            }
            else
            {
                commonFactorInfo = commonFactorInfoBack;
                front = false;
            }

            positions = commonFactorInfo.Positions;
            return commonFactorInfo.GetCommonFactor();
        }

        private static FactorInfo GetFactorInfo(Dictionary<Operator, FactorInfo> dictionary)
        {
            FactorInfo commonFactorInfo = null;

            foreach (KeyValuePair<Operator, FactorInfo> pair in dictionary)
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

            return commonFactorInfo;
        }

        private static void UpdateFactorDictionaryWithOperator(Dictionary<Operator, FactorInfo> dictionary, Operator op, int position)
        {
            if (op.IsSymbol)
            {
                UpdateFactorDictionaryWithSymbol(dictionary, op.SymbolTerm, position);
            }
            else
            {
                UpdateFactorDictionary(dictionary, op, Rational.One, position);
            }
        }

        private static void UpdateFactorDictionaryWithSymbol(Dictionary<Operator, FactorInfo> dictionary, Symbol multiplier, int position)
        {
            foreach (Symbol productTerm in multiplier.ProductTerms)
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

                UpdateFactorDictionary(dictionary, new OperatorSymbol(factor), exponent, position);
            }
        }

        private static void UpdateFactorDictionary(Dictionary<Operator, FactorInfo> dictionary, Operator op, Rational exponent, int position)
        {
            FactorInfo factorInfo = null;
            if (dictionary.TryGetValue(op, out factorInfo))
            {
                factorInfo.Exponent = Rational.Min(factorInfo.Exponent, exponent);
            }
            else
            {
                factorInfo = new FactorInfo(op, exponent);
                dictionary[op] = factorInfo;
            }
            factorInfo.Positions.Add(position);
        }

        public static Operator operator -(Operator lhs, Operator rhs)
        {
            return lhs + (-rhs);
        }

        public static Operator operator *(Operator lhs, Operator rhs)
        {
            return lhs.Multiply(rhs, false);
        }

        public static Symbol operator *(Operator lhs, Symbol rhs)
        {
            return lhs.Operate(rhs);
        }

        public static EuclideanVector3 operator *(Operator lhs, EuclideanVector3 rhs)
        {
            return new EuclideanVector3(i => lhs * rhs[i]);
        }

        public static LorentzVectorU operator *(Operator lhs, LorentzVectorU rhs)
        {
            return new LorentzVectorU(i => lhs * rhs[i]);
        }

        public static Operator operator *(int lhs, Operator rhs)
        {
            return rhs.Multiply(new Constant(lhs), true);
        }

        public static Operator operator *(Symbol lhs, Operator rhs)
        {
            return rhs.Multiply(lhs, true);
        }

        public static Operator operator-(Operator rhs)
        {
            return rhs.Negative();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Operator rhs = obj as Operator;
            if (rhs == null)
                return false;

            return this.ToString() == rhs.ToString();
        }

        public static bool operator ==(Operator lhs, Operator rhs)
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

        public static bool operator !=(Operator lhs, Operator rhs)
        {
            return !(lhs == rhs);
        }

        private class FactorInfo
        {
            public Operator Operator { get; private set; }
            Rational exponent;
            List<int> positions = new List<int>();

            public FactorInfo(Operator op, Rational exponent)
            {
                this.Operator = op;
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

            public Operator GetCommonFactor()
            {
                if (this.Operator.IsSymbol)
                {
                    Symbol symbol = this.Operator.SymbolTerm.RaiseToExponent(new Constant(this.exponent));
                    return new OperatorSymbol(symbol);
                }
                return this.Operator;

            }
        }
    }
}
