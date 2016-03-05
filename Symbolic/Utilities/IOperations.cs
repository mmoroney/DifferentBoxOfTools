using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Utilities
{
    internal interface IOperations<T>
    {
        T Zero { get; }
        T One { get; }
        T Add(T lhs, T rhs);
        T Subtract(T lhs, T rhs);
        T Negative(T value);
        T Multiply(T lhs, T rhs);
        bool Compare(T lhs, T rhs);
        string GetCanonicalString(T value);
    }
}
