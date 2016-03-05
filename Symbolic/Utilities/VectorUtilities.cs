using Symbolic.Matrix;
using Symbolic.Operators;
using Symbolic.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Utilities
{
    internal static class VectorUtilities
    {
        public static Func<int, T> Initializer<T>(params T[] components)
        {
            return i => components[i];
        }

        public static T ScalarProduct<T>(Func<int, T> lhs, Func<int, T> rhs, int size, IOperations<T> operations)
        {
            return VectorUtilities.ScalarProduct(lhs, rhs, size, operations.Zero, (x, y) => operations.Add(x, y), (x, y) => operations.Multiply(x, y));
        }

        public static Symbol ScalarProduct(Func<int, Operator> lhs, Func<int, Symbol> rhs, int size)
        {
            return VectorUtilities.ScalarProduct(lhs, rhs, size, Symbol.Zero, (x, y) => x + y, (x, y) => x * y);
        }

        public static V ScalarProduct<T, U, V>(Func<int, T> lhs, Func<int, U> rhs, int size, V zero, Func<V, V, V> add, Func<T, U, V> multiply)
        {
            V result = zero;
            for (int i = 0; i < size; i++)
            {
                result = add(result, multiply(lhs(i), rhs(i)));
            }

            return result;
        }

        public static Func<int, T> CrossProduct<T>(Func<int, T> lhs, Func<int, T> rhs, IOperations<T> operations)
        {
            return VectorUtilities.CrossProduct(lhs, rhs, (x, y) => operations.Subtract(x, y), (x, y) => operations.Multiply(x, y));
        }

        public static Func<int, Symbol> CrossProduct(Func<int, Operator> lhs, Func<int, Symbol> rhs)
        {
            return VectorUtilities.CrossProduct(lhs, rhs, (x, y) => x - y, (x, y) => x * y);
        }

        public static Func<int, V> CrossProduct<T, U, V>(Func<int, T> lhs, Func<int, U> rhs, Func<V, V, V> subtract, Func<T, U, V> multiply)
        {
            return i =>
            {
                int index1 = (i + 1) % 3;
                int index2 = (i + 2) % 3;
                return subtract(multiply(lhs(index1), rhs(index2)), multiply(lhs(index2), rhs(index1)));
            };
        }
    }
}
