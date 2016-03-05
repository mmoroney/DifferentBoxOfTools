using Symbolic.Matrix;
using Symbolic.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Utilities
{
    internal static class MatrixUtilities
    {
        public static Func<int, int, T> DiagonalInitializer<T>(T zero, params T[] components)
        {
            return (i, j) => i == j ? components[i] : zero;
        }

        public static T ScalarProduct<T>(Func<int, int, T> lhs, Func<int, int, T> rhs, int size, IOperations<T> operations)
        {
            return MatrixUtilities.ScalarProduct(lhs, rhs, size, operations.Zero, (x, y) => operations.Add(x, y), (x, y) => operations.Multiply(x, y));
        }

        public static V ScalarProduct<T, U, V>(Func<int, int, T> lhs, Func<int, int, U> rhs, int size, V zero, Func<V, V, V> add, Func<T, U, V> multiply)
        {
            V result = zero;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result = add(result, multiply(lhs(i, j), rhs(i, j)));
                }
            }

            return result;
        }
        
        public static T Matrix2Determinant<T>(Func<int, int, T> matrix, Func<T, T, T> subtract, Func<T, T, T> multiply)
        {
            return subtract(multiply(matrix(0, 0), matrix(1, 1)), multiply(matrix(0, 1), matrix(1, 0)));
        }

        public static T Matrix3Determinant<T>(Func<int, int, T> matrix, T zero, T one, Func<T, T, T> add, Func<T, T, T> subtract, Func<T, T, T> multiply)
        {
            T determinant = zero;
            for (int i = 0; i < 3; i++)
            {
                T positive = one;
                T negative = one;
                for (int j = 0; j < 3; j++)
                {
                    positive = multiply(positive, matrix(i, (i + j) % 3));
                    negative = multiply(negative, matrix(i, (i - j + 3) % 3));
                }

                determinant = subtract(add(determinant, positive), negative);
            }

            return determinant;
        }

        public static Func<int, int, T> MatrixInverse<T>(Func<int, int, T> matrix, T zero, Func<T, T> reciprocal, Func<T, T, bool> compare)
        {
            return (i, j) =>
            {
                if (i == j)
                {
                    return reciprocal(matrix(i, j));
                }
                else if (compare(matrix(i, j), zero))
                {
                    return zero;
                }
                else
                {
                    throw new NotSupportedException();
                }
            };
        }
        
        public static Func<int, int, T> CreateIdentityMatrix<T>(T zero, T one)
        {
            return (i, j) => (i == j) ? one : zero;
        }

        public static Func<int, int, T> MatrixMultiply<T>(Func<int, int, T> lhs, Func<int, int, T> rhs, int size, IOperations<T> operations)
        {
            return (i, j) =>
            {
                T value = operations.Zero;
                for (int k = 0; k < size; k++)
                {
                    value = operations.Add(value, operations.Multiply(lhs(i, k), rhs(k, j)));
                }
                return value;
            };
        }

        public static Func<int, T> MatrixVectorMultiply<T>(Func<int, int, T> lhs, Func<int, T> rhs, int size, IOperations<T> operations)
        {
            return MatrixUtilities.MatrixVectorMultiply(lhs, rhs, size, operations.Zero, (x, y) => operations.Add(x, y), (x, y) => operations.Multiply(x, y));
        }

        public static Func<int, Operator> MatrixVectorMultiply(Func<int, int, Symbol> lhs, Func<int, Operator> rhs, int size)
        {
            return MatrixUtilities.MatrixVectorMultiply(lhs, rhs, size, Operator.Zero, (x, y) => x + y, (x, y) => x * y);
        }

        public static Func<int, V> MatrixVectorMultiply<T, U, V>(Func<int, int, T> lhs, Func<int, U> rhs, int size, V zero, Func<V, V, V> add, Func<T, U, V> multiply)
        {
            return i =>
            {
                V result = zero;
                for (int j = 0; j < size; j++)
                {
                    result = add(result, multiply(lhs(i, j), rhs(j)));
                }
                return result;
            };
        }

        public static Func<int, T> VectorMatrixMultiply<T>(Func<int, T> lhs, Func<int, int, T> rhs, int size, IOperations<T> operations)
        {
            return MatrixUtilities.VectorMatrixMultiply(lhs, rhs, size, operations.Zero, (x, y) => operations.Add(x, y), (x, y) => operations.Multiply(x, y));
        }

        public static Func<int, Symbol> VectorMatrixMultiply(Func<int, Operator> lhs, Func<int, int, Symbol> rhs, int size)
        {
            return MatrixUtilities.VectorMatrixMultiply(lhs, rhs, size, Symbol.Zero, (x, y) => x + y, (x, y) => x * y);
        }

        public static Func<int, V> VectorMatrixMultiply<T, U, V>(Func<int, T> lhs, Func<int, int, U> rhs, int size, V zero, Func<V, V, V> add, Func<T, U, V> multiply)
        {
            return i =>
            {
                V result = zero;
                for (int j = 0; j < size; j++)
                {
                    result = add(result, multiply(lhs(j), rhs(j, i)));
                }
                return result;
            };
        }

        public static Func<int, int, V> MatrixCrossProduct<T, U, V>(Func<int, T> lhs, Func<int, U> rhs, Func<V, V, V> subtract, Func<T, U, V> multiply)
        {
            return (i, j) => subtract(multiply(lhs(i), rhs(j)), multiply(lhs(j), rhs(i)));
        }
    }
}
