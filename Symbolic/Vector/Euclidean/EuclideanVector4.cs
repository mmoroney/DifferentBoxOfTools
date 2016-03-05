using Symbolic.Algebra;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector4 : EuclideanVector4Base<Symbol, EuclideanVector4>
    {
        public static EuclideanVector4 Zero { get; private set; }

        static EuclideanVector4()
        {
            EuclideanVector4.Zero = new EuclideanVector4(i => Symbol.Zero);
        }

        public EuclideanVector4(Symbol component0, Symbol component1, Symbol component2, Symbol component3)
            : this(VectorUtilities.Initializer(component0, component1, component2, component3)) { }

        public EuclideanVector4(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        public Symbol Length
        {
            get
            {
                return Functions.Sqrt(this.InvariantScalar);
            }
        }

        public EuclideanVector4 Value
        {
            get
            {
                return new EuclideanVector4(i => new Constant(this[i].Value));
            }
        }

        protected override EuclideanVector4 Create(Func<int, Symbol> initializer)
        {
            return new EuclideanVector4(initializer);
        }

        public EuclideanVector4 Diff(Variable variable)
        {
            return new EuclideanVector4(i => variable.Derivative * this[i]);
        }

        public Symbol Div(EuclideanVector4Variable coordinates)
        {
            return coordinates.Del.Dot(this);
        }

        public EuclideanVector4 ParallelComponent(EuclideanVector4 vector)
        {
            return vector * this.Dot(vector) / vector.Dot(vector);
        }

        public EuclideanVector4 PerpendicularComponent(EuclideanVector4 vector)
        {
            return this - this.ParallelComponent(vector);
        }

        public static EuclideanVector4 operator /(EuclideanVector4 lhs, Symbol rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
