using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector3
{
    public class Vector3L : Vector3Base<Symbol, Vector3L, Vector3U>
    {
        public Vector3L(Symbol component0, Symbol component1, Symbol component2)
            : this(VectorUtilities.Initializer(component0, component1, component2)) { }

        public Vector3L(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        protected override Vector3L Create(Func<int, Symbol> initializer)
        {
            return new Vector3L(initializer);
        }
    }
}
