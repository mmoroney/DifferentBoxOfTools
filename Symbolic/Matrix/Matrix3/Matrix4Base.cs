using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix3
{
    public abstract class Matrix3Base<TScalar, TMatrix, TInvert> : MetricMatrixBase<TScalar, TMatrix, TInvert>
        where TMatrix : Matrix3Base<TScalar, TMatrix, TInvert>
        where TInvert : Matrix3Base<TScalar, TInvert, TMatrix>
    {
        internal Matrix3Base(IOperations<TScalar> operations, Func<int, int, TScalar> initializer) : base(operations, 3, initializer) { }
    }
}
