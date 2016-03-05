using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Utilities
{
    public static class ArrayUtilities
    {
        public static T[,] Initialize<T>(int len0, int len1, Func<int, int, T> initializer)
        {
            T[,] data = new T[len0, len1];
            for (int i = 0; i < len0; i++)
            {
                for (int j = 0; j < len1; j++)
                {
                    data[i, j] = initializer(i, j);
                }
            }
            return data;
        }

        public static T[, , ,] Initialize<T>(int len0, int len1, int len2, int len3, Func<int, int, int, int, T> initializer)
        {
            T[, , ,] data = new T[len0, len1, len2, len3];
            for (int i = 0; i < len0; i++)
            {
                for (int j = 0; j < len1; j++)
                {
                    for (int k = 0; k < len2; k++)
                    {
                        for (int l = 0; l < len3; l++)
                        {
                            data[i, j, k, l] = initializer(i, j, k, l);
                        }
                    }
                }
            }
            return data;
        }
    }
}
