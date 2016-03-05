using Symbolic.Algebra;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector3 : EuclideanVector3Base<Symbol, EuclideanVector3>
    {
        public static EuclideanVector3 Zero { get; private set; }

        static EuclideanVector3()
        {
            EuclideanVector3.Zero = new EuclideanVector3(i => Symbol.Zero);
        }

        public EuclideanVector3() : this(Symbol.Zero) { }
        public EuclideanVector3(Symbol component0) : this(component0, Symbol.Zero) { }
        public EuclideanVector3(Symbol component0, Symbol component1) : this(component0, component1, Symbol.Zero) { }
        public EuclideanVector3(Symbol component0, Symbol component1, Symbol component2) 
            : this(VectorUtilities.Initializer(component0, component1, component2)) { }

        public EuclideanVector3(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        public Symbol Length
        {
            get
            {
                return Functions.Sqrt(this.InvariantScalar);
            }
        }

        public EuclideanVector3 Value
        {
            get
            {
                return new EuclideanVector3(i => new Constant(this[i].Value));
            }
        }

        protected override EuclideanVector3 Create(Func<int, Symbol> initializer)
        {
            return new EuclideanVector3(initializer);
        }

        public EuclideanVector3 Diff(Variable variable)
        {
            return new EuclideanVector3(i => variable.Derivative * this[i]);
        }

        public Symbol Div(EuclideanVector3Variable coordinates)
        {
            return coordinates.Del.Dot(this);
        }

        public EuclideanVector3 ParallelComponent(EuclideanVector3 vector)
        {
            return vector * this.Dot(vector) / vector.Dot(vector);
        }

        public EuclideanVector3 PerpendicularComponent(EuclideanVector3 vector)
        {
            return this - this.ParallelComponent(vector);
        }

        public static EuclideanVector3 operator /(EuclideanVector3 lhs, Symbol rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
