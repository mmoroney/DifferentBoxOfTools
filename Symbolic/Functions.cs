using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbolic.Algebra;
using Symbolic.Vector;
using Symbolic.Operators;
using Symbolic.Trigonometry;

namespace Symbolic
{
    public static class Functions
    {
        public static Symbol Pow(Symbol argument, Symbol exponent)
        {
            return argument.RaiseToExponent(exponent);
        }

        public static Symbol Pow(Symbol argument, int exponent)
        {
            return Functions.Pow(argument, new Constant(exponent));
        }

        public static Symbol Pow(int argument, Symbol exponent)
        {
            return Functions.Pow(new Constant(argument), exponent);
        }

        public static Symbol Sqrt(Symbol symbol)
        {
            return symbol.RaiseToExponent(new Constant(1, 2));
        }

        public static Symbol Log(Symbol symbol)
        {
            return symbol.Log();
        }

        public static Symbol Sin(Symbol symbol)
        {
            return new Sine(symbol);
        }

        public static Symbol Cos(Symbol symbol)
        {
            return new Cosine(symbol);
        }
    }
}
