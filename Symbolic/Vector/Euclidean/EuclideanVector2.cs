using Symbolic.Algebra;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Vector.Euclidean
{
    public class EuclideanVector2 : EuclideanVector2Base<Symbol, EuclideanVector2>
    {
        public static EuclideanVector2 Zero { get; private set; }

        static EuclideanVector2()
        {
            EuclideanVector2.Zero = new EuclideanVector2(i => Symbol.Zero);
        }

        public EuclideanVector2(Symbol component0, Symbol component1) : this(VectorUtilities.Initializer(component0, component1)) { }
        public EuclideanVector2(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        public Symbol Length
        {
            get
            {
                return Functions.Sqrt(this.InvariantScalar);
            }
        }

        public EuclideanVector2 Value
        {
            get
            {
                return new EuclideanVector2(i => new Constant(this[i].Value));
            }
        }

        protected override EuclideanVector2 Create(Func<int, Symbol> initializer)
        {
            return new EuclideanVector2(initializer);
        }

        public EuclideanVector2 Diff(Variable variable)
        {
            return new EuclideanVector2(i => variable.Derivative * this[i]);
        }

        public Symbol Div(EuclideanVector2Variable coordinates)
        {
            return coordinates.Del.Dot(this);
        }

        public EuclideanVector2 ParallelComponent(EuclideanVector2 vector)
        {
            return vector * this.Dot(vector) / vector.Dot(vector);
        }

        public EuclideanVector2 PerpendicularComponent(EuclideanVector2 vector)
        {
            return this - this.ParallelComponent(vector);
        }

        public static EuclideanVector2 operator /(EuclideanVector2 lhs, Symbol rhs)
        {
            return lhs * (1 / rhs);
        }
    }
}
