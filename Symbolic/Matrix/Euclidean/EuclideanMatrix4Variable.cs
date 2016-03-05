using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Matrix
{
    public class EuclideanMatrix4Variable : EuclideanMatrix4
    {
        Variable[,] variables;

        public EuclideanMatrix4Variable(Func<int, int, Variable> initializer): base(initializer)
        {
            this.variables = ArrayUtilities.Initialize(4, 4, initializer);
        }

        public void SetValue(int row, int column, Rational value)
        {
            this.variables[row, column].SetValue(value);
        }
    }
}
