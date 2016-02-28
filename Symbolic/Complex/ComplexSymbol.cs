using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Complex
{
    public class ComplexSymbol
    {
        public Symbol Real { get; private set; }
        public Symbol Imaginary { get; private set; }

        private static ComplexSymbol zero = new ComplexSymbol(Symbol.Zero);
        private static ComplexSymbol one = new ComplexSymbol(Symbol.One);
        private static ComplexSymbol i = new ComplexSymbol(Symbol.Zero, Symbol.One);

        public static ComplexSymbol Zero
        {
            get { return ComplexSymbol.zero; }
        }

        public static ComplexSymbol One
        {
            get { return ComplexSymbol.one; }
        }

        public static ComplexSymbol I
        {
            get { return ComplexSymbol.i; }
        }
        
        public ComplexSymbol() : this(Symbol.Zero) { }
        public ComplexSymbol(Symbol real) : this(real, Symbol.Zero) { }
        public ComplexSymbol(Symbol real, Symbol imaginary)
        {
            this.Real = real;
            this.Imaginary = imaginary;
        }

        public Complex Value
        {
            get
            {
                return new Complex(this.Real.Value, this.Imaginary.Value);
            }
        }

        public ComplexSymbol Diff(Variable variable)
        {
            return new ComplexSymbol(this.Real.Diff(variable), this.Imaginary.Diff(variable));
        }

        public ComplexSymbol Conjugate
        {
            get
            {
                return new ComplexSymbol(this.Real, -this.Imaginary);
            }
        }

        public override string ToString()
        {
            if (this.Real == Symbol.Zero && this.Imaginary == Symbol.Zero)
            {
                return "0";
            }

            StringBuilder builder = new StringBuilder();

            if (this.Real != Symbol.Zero)
            {
                builder.Append(this.Real);
                builder.Append('+');
            }

            if (this.Imaginary != Symbol.Zero)
            {
                if (this.Imaginary == -Symbol.One)
                {
                    builder.Append('-');
                }
                else if (this.Imaginary != Symbol.One)
                {
                    builder.Append(this.Imaginary);
                }
                builder.Append('i');
            }

            return builder.ToString();
        }

        public static ComplexSymbol operator -(ComplexSymbol c)
        {
            return new ComplexSymbol(-c.Real, -c.Imaginary);
        }

        public static ComplexSymbol operator +(ComplexSymbol a, ComplexSymbol b)
        {
            return new ComplexSymbol(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static ComplexSymbol operator +(ComplexSymbol a, int b)
        {
            return new ComplexSymbol(a.Real + b, a.Imaginary);
        }

        public static ComplexSymbol operator +(int a, ComplexSymbol b)
        {
            return new ComplexSymbol(a + b.Real, b.Imaginary);
        }

        public static ComplexSymbol operator -(ComplexSymbol a, ComplexSymbol b)
        {
            return new ComplexSymbol(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public static ComplexSymbol operator -(ComplexSymbol a, int b)
        {
            return new ComplexSymbol(a.Real - b, a.Imaginary);
        }

        public static ComplexSymbol operator -(int a, ComplexSymbol b)
        {
            return new ComplexSymbol(a - b.Real, -b.Imaginary);
        }

        public static ComplexSymbol operator *(ComplexSymbol a, ComplexSymbol b)
        {
            return new ComplexSymbol(a.Real * b.Real - a.Imaginary * b.Imaginary,
                a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        public static ComplexSymbol operator *(ComplexSymbol a, int b)
        {
            return new ComplexSymbol(a.Real * b, a.Imaginary * b);
        }

        public static ComplexSymbol operator *(int a, ComplexSymbol b)
        {
            return new ComplexSymbol(a * b.Real, a * b.Imaginary);
        }

        public static ComplexSymbol operator /(ComplexSymbol a, ComplexSymbol b)
        {
            Symbol denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new ComplexSymbol((a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator,
                (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator);
        }

        public static ComplexSymbol operator /(ComplexSymbol a, int b)
        {
            int denominator = b * b;
            return new ComplexSymbol(a.Real / b, a.Imaginary / b);
        }

        public static ComplexSymbol operator /(int a, ComplexSymbol b)
        {
            Symbol denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
            return new ComplexSymbol(a * b.Real / denominator, -a * b.Imaginary / denominator);
        }

        public override int GetHashCode()
        {
            return this.Real.GetHashCode() ^ this.Imaginary.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ComplexSymbol rhs = obj as ComplexSymbol;
            if (rhs == null)
            {
                return false;
            }

            return this.Real == rhs.Real && this.Imaginary == rhs.Imaginary;
        }

        public static bool operator ==(ComplexSymbol lhs, ComplexSymbol rhs)
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

        public static bool operator !=(ComplexSymbol lhs, ComplexSymbol rhs)
        {
            return !(lhs == rhs);
        }
    }
}
