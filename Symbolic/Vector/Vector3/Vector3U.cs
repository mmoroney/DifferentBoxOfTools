using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector3
{
    public class Vector3U : Vector3Base<Symbol, Vector3U, Vector3L>
    {
        public Vector3U(Symbol component0, Symbol component1, Symbol component2)
            : this(VectorUtilities.Initializer(component0, component1, component2)) { }

        public Vector3U(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        protected override Vector3U Create(Func<int, Symbol> initializer)
        {
            return new Vector3U(initializer);
        }
    }
}
