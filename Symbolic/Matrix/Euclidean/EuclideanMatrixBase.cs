using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Matrix.Euclidean
{
    public abstract class EuclideanMatrixBase<TScalar, TMatrix> : MatrixBase<TScalar, TMatrix, TMatrix>
        where TMatrix : EuclideanMatrixBase<TScalar, TMatrix>
    {
        internal EuclideanMatrixBase(IOperations<TScalar> operations, int size, Func<int, int, TScalar> initializer)
            : base(operations, size, initializer) { }

        public TScalar InvariantScalar
        {
            get
            {
                return this.Dot(this);
            }
        }
    }
}
