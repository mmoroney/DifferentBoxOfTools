using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector2
{
    public class Vector2L : Vector2Base<Symbol, Vector2L, Vector2U>
    {
        public Vector2L(Symbol component0, Symbol component1)
            : this(VectorUtilities.Initializer(component0, component1)) { }

        public Vector2L(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        protected override Vector2L Create(Func<int, Symbol> initializer)
        {
            return new Vector2L(initializer);
        }
    }
}
