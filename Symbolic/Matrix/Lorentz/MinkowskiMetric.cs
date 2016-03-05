using Symbolic.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Lorentz
{
    public static class MinkowskiMetric
    {
        public static LorentzMatrixLL Covariant { get; private set; }
        public static LorentzMatrixUU Contravariant { get; private set; }

        static MinkowskiMetric() 
        {
            MinkowskiMetric.Covariant = new LorentzMatrixLL(-Symbol.One, Symbol.One, Symbol.One, Symbol.One);
            MinkowskiMetric.Contravariant = MinkowskiMetric.Covariant.Invert();
        }
    }
}
