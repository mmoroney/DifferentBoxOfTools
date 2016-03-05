using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix2
{
    public abstract class Matrix2Base<TScalar, TMatrix, TInvert> : MetricMatrixBase<TScalar, TMatrix, TInvert>
        where TMatrix : Matrix2Base<TScalar, TMatrix, TInvert>
        where TInvert : Matrix2Base<TScalar, TInvert, TMatrix>
    {
        internal Matrix2Base(IOperations<TScalar> operations, Func<int, int, TScalar> initializer) : base(operations, 2, initializer) { }
    }
}
