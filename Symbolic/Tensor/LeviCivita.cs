using Symbolic.Matrix;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Tensor
{
    public static class LeviCivita
    {
        public static EuclideanMatrix2 Two { get; private set; }
        public static Tensor4D4 Four { get; private set; }

        static LeviCivita() 
        {
            Symbol[,] array2 = ArrayUtilities.Initialize(2, 2, (i, j) => Symbol.Zero);
            LeviCivita.Permute(2, (indices, symbol) => array2[indices[0], indices[1]] = symbol);
            LeviCivita.Two = new EuclideanMatrix2((i, j) => array2[i, j]);

            Symbol[, , ,] array4 = ArrayUtilities.Initialize(4, 4, 4, 4, (i, j, k, l) => Symbol.Zero);
            LeviCivita.Permute(4, (indices, symbol) => array4[indices[0], indices[1], indices[2], indices[3]] = symbol);
            LeviCivita.Four = new Tensor4D4((i, j, k, l) => array4[i, j, k, l]);
        }

        private static void Permute(int length, Action<int[], Symbol> action)
        {
            int[] indices = new int[length];
            for (int i = 0; i < length; i++)
            {
                indices[i] = i;
            }

            LeviCivita.Permute(indices, 0, true, action);
        }

        private static void Permute(int[] indices, int position, bool parity, Action<int[], Symbol> action)
        {
            if (position == indices.Length - 1)
            {
                action(indices, parity ? Symbol.One : -Symbol.One);
                return;
            }

            for (int i = position; i < indices.Length; i++)
            {
                Swap(indices, position, i);
                Permute(indices, position + 1, parity ^ (i != position), action);
                Swap(indices, position, i);
            }
        }

        private static void Swap(int[] indices, int i, int j)
        {
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }
    }
}
