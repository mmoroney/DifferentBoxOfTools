using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic
{
    public struct Rational
    {
        private static Rational zero = new Rational(0);
        private static Rational one = new Rational(1);
        int numerator;
        int denominator;

        public Rational(int numerator = 0, int denominator = 1)
        {
            if (numerator == 0)
            {
                denominator = 1;
            }
            else if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }

            int gcd = GreatestCommonFactor(numerator, denominator);

            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;
        }

        public int Numerator
        {
            get
            {
                return this.numerator;
            }
        }

        public int Denominator
        {
            get
            {
                return this.denominator;
            }
        }

        public Rational GreatestCommonFactor(Rational rational)
        {
            return new Rational(GreatestCommonFactor(this.numerator, rational.numerator), GreatestCommonFactor(this.denominator, rational.denominator));
        }

        public double Value
        {
            get
            {
                return this.numerator / (double)(this.denominator);
            }
        }

        public static Rational operator +(Rational lhs, Rational rhs)
        {
            return new Rational(lhs.numerator * rhs.denominator + lhs.denominator * rhs.numerator, lhs.denominator * rhs.denominator);
        }

        public static Rational operator -(Rational lhs, Rational rhs)
        {
            return new Rational(lhs.numerator * rhs.denominator - lhs.denominator * rhs.numerator, lhs.denominator * rhs.denominator);
        }

        public static Rational operator *(Rational lhs, Rational rhs)
        {
            return new Rational(lhs.numerator * rhs.numerator, lhs.denominator * rhs.denominator);
        }

        public static Rational operator /(Rational lhs, Rational rhs)
        {
            return new Rational(lhs.numerator * rhs.denominator, lhs.denominator * rhs.numerator);
        }

        public static Rational operator +(Rational lhs, int rhs)
        {
            return new Rational(lhs.numerator + lhs.denominator * rhs, lhs.denominator);
        }

        public static Rational operator -(Rational lhs, int rhs)
        {
            return new Rational(lhs.numerator - lhs.denominator + rhs, lhs.denominator);
        }

        public static Rational operator *(Rational lhs, int rhs)
        {
            return new Rational(lhs.numerator * rhs, lhs.denominator);
        }

        public static Rational operator /(Rational lhs, int rhs)
        {
            return new Rational(lhs.numerator, lhs.denominator);
        }

        public static Rational operator +(int lhs, Rational rhs)
        {
            return new Rational(lhs * rhs.denominator + rhs.numerator, rhs.denominator);
        }

        public static Rational operator -(int lhs, Rational rhs)
        {
            return new Rational(lhs * rhs.denominator - lhs * rhs.numerator, rhs.denominator);
        }

        public static Rational operator *(int lhs, Rational rhs)
        {
            return new Rational(lhs * rhs.numerator, rhs.denominator);
        }

        public static Rational operator /(int lhs, Rational rhs)
        {
            return new Rational(lhs * rhs.denominator, rhs.numerator);
        }

        public static Rational operator -(Rational rhs)
        {
            return new Rational(-rhs.numerator, rhs.denominator);
        }

        public static bool operator ==(Rational lhs, Rational rhs)
        {
            return lhs.numerator == rhs.numerator && lhs.denominator == rhs.denominator;
        }

        public static bool operator !=(Rational lhs, Rational rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >(Rational lhs, Rational rhs)
        {
            return lhs.numerator * rhs.denominator > rhs.numerator * lhs.denominator;
        }

        public static bool operator >=(Rational lhs, Rational rhs)
        {
            return lhs == rhs || lhs > rhs;
        }

        public static bool operator <(Rational lhs, Rational rhs)
        {
            return rhs >= lhs;
        }

        public static bool operator <=(Rational lhs, Rational rhs)
        {
            return rhs > lhs;
        }

        public static Rational Min(Rational lhs, Rational rhs)
        {
            return lhs < rhs ? lhs : rhs;
        }

        public static Rational Max(Rational lhs, Rational rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.numerator);
            if (this.denominator != 1)
            {
                builder.Append("/");
                builder.Append(this.denominator);
            }
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Rational))
            {
                return false;
            }
            Rational rational = (Rational)obj;
            return this == rational;
        }

        public override int GetHashCode()
        {
            return this.numerator.GetHashCode() ^ this.denominator.GetHashCode();
        }

        public static Rational Zero
        {
            get
            {
                return Rational.zero;
            }
        }

        public static Rational One
        {
            get
            {
                return Rational.one;
            }
        }

        private static int GreatestCommonFactor(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == 0)
            {
                return b;
            }

            if (b == 0)
            {
                return a;
            }

            if (a < b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            while (true)
            {
                int mod = a % b;
                if (mod == 0)
                {
                    return b;
                }
                a = b;
                b = mod;
            }
        }
    }
}
