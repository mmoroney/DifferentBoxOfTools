using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Complex
{
    public struct Complex
    {
        Rational real;
        Rational imaginary;

        private static Complex zero = new Complex(0);
        private static Complex one = new Complex(1);
        private static Complex i = new Complex(0, 1);

        public static Complex Zero
        {
            get { return Complex.zero; }
        }

        public static Complex One
        {
            get { return Complex.one; }
        }

        public static Complex I
        {
            get { return Complex.i; }
        }

        public Rational Real
        {
            get { return this.real; }
            set { this.real = value; }
        }

        public Rational Imaginary
        {
            get { return this.imaginary; }
            set { this.imaginary = value; }
        }

        public Complex(int real)
            : this(new Rational(real), Rational.Zero)
        {
        }

        public Complex(int real, int imaginary)
            : this(new Rational(real), new Rational(imaginary))
        {
        }

        public Complex(Rational real, Rational imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public Complex Conjugate
        {
            get
            {
                return new Complex(this.real, -this.imaginary);
            }
        }

        public override string ToString()
        {
            if (this.real == Rational.Zero && this.imaginary == Rational.Zero)
            {
                return "0";
            }

            StringBuilder builder = new StringBuilder();

            if (this.real != Rational.Zero)
            {
                builder.Append(this.real);
                if (this.imaginary > Rational.Zero)
                {
                    builder.Append('+');
                }
            }

            if (this.imaginary != Rational.Zero)
            {
                if (this.imaginary == -Rational.One) 
                {
                    builder.Append('-');
                }
                else if (this.imaginary != Rational.One)
                {
                    builder.Append(this.imaginary);
                }
                builder.Append('i');
            }

            return builder.ToString();
        }

        public static Complex operator -(Complex c)
        {
            return new Complex(-c.real, -c.imaginary);
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.real + b.real, a.imaginary + b.imaginary);
        }

        public static Complex operator +(Complex a, Rational b)
        {
            return new Complex(a.real + b, a.imaginary);
        }

        public static Complex operator +(Rational a, Complex b)
        {
            return new Complex(a + b.real, b.imaginary);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.real - b.real, a.imaginary - b.imaginary);
        }

        public static Complex operator -(Complex a, Rational b)
        {
            return new Complex(a.real - b, a.imaginary);
        }

        public static Complex operator -(Rational a, Complex b)
        {
            return new Complex(a - b.real, -b.imaginary);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.real * b.real - a.imaginary * b.imaginary,
                a.real * b.imaginary + a.imaginary * b.real);
        }

        public static Complex operator *(Complex a, Rational b)
        {
            return new Complex(a.real * b, a.imaginary * b);
        }

        public static Complex operator *(Rational a, Complex b)
        {
            return new Complex(a * b.real, a * b.imaginary);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            Rational denominator = b.real * b.real + b.imaginary * b.imaginary;

            return new Complex((a.real * b.real + a.imaginary * b.imaginary) / denominator,
                (a.imaginary * b.real - a.real * b.imaginary) / denominator);
        }

        public static Complex operator /(Complex a, Rational b)
        {
            Rational denominator = b * b;
            return new Complex(a.real / b, a.imaginary / b);
        }

        public static Complex operator /(Rational a, Complex b)
        {
            Rational denominator = b.real * b.real + b.imaginary * b.imaginary;
            return new Complex(a * b.real / denominator, -a * b.imaginary / denominator);
        }
    }
}
