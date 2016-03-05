using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector.Vector4
{
    public class Vector4U : Vector4Base<Symbol, Vector4U, Vector4L>
    {
        public Vector4U(Symbol component0, Symbol component1, Symbol component2, Symbol component3)
            : this(VectorUtilities.Initializer(component0, component1, component2, component3)) { }

        public Vector4U(Func<int, Symbol> initializer) : base(Symbol.Operations, initializer) { }

        protected override Vector4U Create(Func<int, Symbol> initializer)
        {
            return new Vector4U(initializer);
        }
    }
}
