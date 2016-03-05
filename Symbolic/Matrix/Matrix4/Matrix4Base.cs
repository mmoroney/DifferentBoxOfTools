using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Matrix4
{
    public abstract class Matrix4Base<TScalar, TMatrix, TInvert> : MetricMatrixBase<TScalar, TMatrix, TInvert>
        where TMatrix : Matrix4Base<TScalar, TMatrix, TInvert>
        where TInvert : Matrix4Base<TScalar, TInvert, TMatrix>
    {
        internal Matrix4Base(IOperations<TScalar> operations, Func<int, int, TScalar> initializer) : base(operations, 4, initializer) { }
    }
}
