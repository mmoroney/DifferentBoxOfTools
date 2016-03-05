using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolic.Matrix
{
    public class EuclideanMatrix2Variable : EuclideanMatrix4
    {
        Variable[,] variables;

        public EuclideanMatrix2Variable(Func<int, int, Variable> initializer) : base(initializer)
        {
            this.variables = ArrayUtilities.Initialize(2, 2, initializer);
        }

        public void SetValue(int row, int column, Rational value)
        {
            this.variables[row, column].SetValue(value);
        }
    }
}
