using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix
{
    public abstract class MetricMatrixBase<TScalar, TMatrix, TInvert> : MatrixBase<TScalar, TMatrix, TInvert>
        where TMatrix : MatrixBase<TScalar, TMatrix, TInvert>
        where TInvert : MatrixBase<TScalar, TInvert, TMatrix>
    {
        internal MetricMatrixBase(IOperations<TScalar> operations, int size, Func<int, int, TScalar> initializer) : base(operations, size, initializer) { }

        public TScalar Dot(MetricMatrixBase<TScalar, TInvert, TMatrix> matrix)
        {
            return MatrixUtilities.ScalarProduct((i, j) => this[i, j], (i, j) => matrix[i, j], this.Size, this.Operations);
        }    
    }
}
