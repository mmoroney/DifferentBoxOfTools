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
    public class LorentzVectorL : LorentzVectorBase<Symbol, LorentzVectorL, EuclideanVector3, LorentzVectorU>
    {
        public static LorentzVectorL Zero { get; private set; }

        static LorentzVectorL()
        {
            LorentzVectorL.Zero = new LorentzVectorL(i => Symbol.Zero);
        }

        public LorentzVectorL(Symbol scalar, EuclideanVector3 vector) : this(scalar, vector[0], vector[1], vector[2]) { }
        public LorentzVectorL() : this(Symbol.Zero) { }
        public LorentzVectorL(Symbol component0) : this(component0, EuclideanVector3.Zero) { }
        public LorentzVectorL(Symbol component0, Symbol component1) : this(component0, component1, Symbol.Zero) { }
        public LorentzVectorL(Symbol component0, Symbol component1, Symbol component2) : this(component0, component1, component2, Symbol.Zero) { }
        public LorentzVectorL(Symbol component0, Symbol component1, Symbol component2, Symbol component3)
            : this(VectorUtilities.Initializer(component0, component1, component2, component3)) { }

        public LorentzVectorL(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        public LorentzVectorL Value
        {
            get
            {
                return new LorentzVectorL(i => new Constant(this[i].Value));
            }
        }

        public LorentzVectorL Transform(EuclideanVector3 velocity)
        {
            return this * LorentzTransform.Matrix(velocity);
        }
        
        protected override LorentzVectorL Create(Func<int, Symbol> initializer)
        {
            return new LorentzVectorL(initializer);
        }

        protected override EuclideanVector3 CreateVector3(Func<int, Symbol> initializer)
        {
            return new EuclideanVector3(initializer);
        }

        protected override LorentzVectorU CreateInvert(Func<int, Symbol> initializer)
        {
            return new LorentzVectorU(initializer);
        }

        public LorentzVectorL Diff(Variable variable)
        {
            return new LorentzVectorL(i => variable.Derivative * this[i]);
        }

        public EuclideanMatrix4 Curl(LorentzVectorOperatorL del)
        {
            return new EuclideanMatrix4(MatrixUtilities.MatrixCrossProduct(i => del[i], i => this[i], (x, y) => x - y, (x, y) => x * y));
        }

        public static LorentzVectorL operator /(LorentzVectorL lhs, Symbol rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
