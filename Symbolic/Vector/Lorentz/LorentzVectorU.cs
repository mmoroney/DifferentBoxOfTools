using Symbolic.Algebra;
using Symbolic.Matrix;
using Symbolic.Matrix.Lorentz;
using Symbolic.Utilities;
using Symbolic.Vector.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Lorentz
{
    public class LorentzVectorU : LorentzVectorBase<Symbol, LorentzVectorU, EuclideanVector3, LorentzVectorL>
    {
        public static LorentzVectorU Zero { get; private set; }

        static LorentzVectorU()
        {
            LorentzVectorU.Zero = new LorentzVectorU(i => Symbol.Zero);
        }
        
        public LorentzVectorU(Symbol scalar, EuclideanVector3 vector) : this(scalar, vector[0], vector[1], vector[2]) { }
        public LorentzVectorU() : this(Symbol.Zero) { }
        public LorentzVectorU(Symbol component0) : this(component0, EuclideanVector3.Zero) { }
        public LorentzVectorU(Symbol component0, Symbol component1) : this(component0, component1, Symbol.Zero) { }
        public LorentzVectorU(Symbol component0, Symbol component1, Symbol component2) : this(component0, component1, component2, Symbol.Zero) { }
        public LorentzVectorU(Symbol component0, Symbol component1, Symbol component2, Symbol component3)
            : this(VectorUtilities.Initializer(component0, component1, component2, component3)) { }

        public LorentzVectorU(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        public LorentzVectorU Value
        {
            get
            {
                return new LorentzVectorU(i => new Constant(this[i].Value));
            }
        }

        public LorentzVectorU Transform(EuclideanVector3 velocity)
        {
            return LorentzTransform.Matrix(velocity) * this;
        }

        protected override LorentzVectorU Create(Func<int, Symbol> initializer)
        {
            return new LorentzVectorU(initializer);
        }

        protected override EuclideanVector3 CreateVector3(Func<int, Symbol> initializer)
        {
            return new EuclideanVector3(initializer);
        }

        protected override LorentzVectorL CreateInvert(Func<int, Symbol> initializer)
        {
            return new LorentzVectorL(initializer);
        }

        public LorentzVectorU Diff(Variable variable)
        {
            return new LorentzVectorU(i => variable.Derivative * this[i]);
        }

        public LorentzMatrixUU Curl(LorentzVectorOperatorU del)
        {
            return new LorentzMatrixUU(MatrixUtilities.MatrixCrossProduct(i => del[i], i => this[i], (x, y) => x - y, (x, y) => x * y));
        }

        public static LorentzVectorU operator /(LorentzVectorU lhs, Symbol rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
