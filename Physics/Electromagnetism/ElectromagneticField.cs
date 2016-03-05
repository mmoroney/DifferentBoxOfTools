 using Symbolic;
using Physics.SpecialRelativity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbolic.Vector;
using Symbolic.Matrix;
using Symbolic.Tensor;
using Symbolic.Vector.Lorentz;
using Symbolic.Vector.Euclidean;
using Symbolic.Matrix.Lorentz;

namespace Physics.Electromagnetism
{
    public class ElectromagneticField
    {
        public LorentzVectorU Potential { get; private set; }
        public LorentzVectorOperatorL Del { get; private set; }
        public EuclideanVector3 ElectricField { get; private set; }
        public EuclideanVector3 MagneticField { get; private set; }
        public LorentzMatrixUU FieldStrengthTensor { get; private set; }
        public LorentzMatrixLL DualFieldStrengthTensor { get; private set; }
        public LorentzVectorU CurrentDensity { get; private set; }

        public ElectromagneticField(LorentzVectorU potential, LorentzVectorOperatorL del)
        {
            this.Potential = potential;
            this.Del = del;

            this.ElectricField = -this.Del.Vector * potential.Scalar - this.Del.Scalar * potential.Vector;
            this.MagneticField = this.Del.Vector.Cross(potential.Vector);
            this.FieldStrengthTensor = potential.Curl(this.Del.Invert());
            this.CurrentDensity = this.Del * this.FieldStrengthTensor;
            this.DualFieldStrengthTensor = new LorentzMatrixLL((i, j) =>
            {
                Symbol symbol = Symbol.Zero;
                for (int k = 0; k < 4; k++)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        symbol = symbol - LeviCivita.Four[i, j, k, l] * this.FieldStrengthTensor[k, l] / 2;
                    }
                }
                return symbol;
            });
        }

        public ElectromagneticField Transform(EuclideanVector3 velocity)
        {
            LorentzMatrixUL L = LorentzTransform.Matrix(velocity);
            return new ElectromagneticField(L * this.Potential, L.Invert() * this.Del);
        }
    }
}
