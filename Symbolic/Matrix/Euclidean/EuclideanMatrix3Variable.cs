using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Matrix
{
    public class EuclideanMatrix3Variable : EuclideanMatrix3
    {
        Variable[,] variables;

        public EuclideanMatrix3Variable(Func<int, int, Variable> initializer) : base(initializer)
        {
            this.variables = ArrayUtilities.Initialize(3, 3, initializer);
        }

        public void SetValue(int row, int column, Rational value)
        {
            this.variables[row, column].SetValue(value);
        }
    }
}
