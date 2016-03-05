using Symbolic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbolic.Vector;
using Symbolic.Matrix;
using Symbolic.Vector.Lorentz;
using Symbolic.Vector.Euclidean;
using Symbolic.Matrix.Lorentz;

namespace Symbolic.Matrix.Lorentz
{
    public static class LorentzTransform
    {
        public static Symbol Gamma(EuclideanVector3 velocity)
        {
            return 1 / Functions.Sqrt(1 - velocity.Dot(velocity));
        }

        public static LorentzMatrixUL Matrix(EuclideanVector3 velocity)
        {
            return new LorentzMatrixUL((i, j) =>
            {
                Symbol gamma = LorentzTransform.Gamma(velocity);

                if (i == 0)
                {
                    if (j == 0)
                    {
                        return gamma;
                    }
                    
                    return -gamma * velocity[j - 1];
                }

                if (j == 0)
                {
                    return -gamma * velocity[i - 1];
                }
                
                Symbol symbol = (gamma - 1) * velocity[i - 1] * velocity[j - 1] / velocity.InvariantScalar;
                if (i == j)
                {
                    symbol = 1 + symbol;
                }
                return symbol;
            });
        }
    }
}
