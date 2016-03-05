using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector2
{
    public class Vector2U : Vector2Base<Symbol, Vector2U, Vector2L>
    {
        public Vector2U(Symbol component0, Symbol component1)
            : this(VectorUtilities.Initializer(component0, component1)) { }

        public Vector2U(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        protected override Vector2U Create(Func<int, Symbol> initializer)
        {
            return new Vector2U(initializer);
        }
    }
}
