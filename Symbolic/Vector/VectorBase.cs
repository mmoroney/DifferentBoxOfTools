using Symbolic.Algebra;
using Symbolic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbolic.Vector
{
    public abstract class VectorBase<TScalar, TVector, TInvert>
        where TVector : VectorBase<TScalar, TVector, TInvert>
        where TInvert : VectorBase<TScalar, TInvert, TVector>
    {
        private TScalar[] components;
        internal IOperations<TScalar> Operations { get; private set; }

        internal VectorBase(IOperations<TScalar> operations, int size, Func<int, TScalar> initializer)
        {
            this.Operations = operations;
            this.components = new TScalar[size];
            for (int i = 0; i < size; i++)
            {
                this.components[i] = initializer(i);
            }
        }

        public TScalar Dot(VectorBase<TScalar, TInvert, TVector> vector)
        {
            return VectorUtilities.ScalarProduct(i => this[i], i => vector[i], this.Size, this.Operations);
        }
        
        public int Size
        {
            get
            {
                return this.components.Length;
            }
        }

        public TScalar this[int i]
        {
            get
            {
                return this.components[i];
            }
        }

        protected abstract TVector Create(Func<int, TScalar> initializer);

        protected string BuildVectorString(Func<TScalar, string> getComponentString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (TScalar component in this.components)
            {
                if (stringBuilder.Length == 0)
                {
                    stringBuilder.Append("[");
                }
                else
                {
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append(getComponentString(component));
            }

            stringBuilder.Append("]");

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return this.BuildVectorString(x => x.ToString());
        }

        public static TVector operator +(VectorBase<TScalar, TVector, TInvert> lhs, VectorBase<TScalar, TVector, TInvert> rhs)
        {
            return lhs.Create(i => lhs.Operations.Add(lhs[i], rhs[i]));
        }

        public static TVector operator -(VectorBase<TScalar, TVector, TInvert> lhs, VectorBase<TScalar, TVector, TInvert> rhs)
        {
            return lhs.Create(i => lhs.Operations.Subtract(lhs[i], rhs[i]));
        }

        public static TVector operator *(VectorBase<TScalar, TVector, TInvert> lhs, TScalar rhs)
        {
            return lhs.Create(i => lhs.Operations.Multiply(lhs[i], rhs));
        }

        public static TVector operator *(TScalar lhs, VectorBase<TScalar, TVector, TInvert> rhs)
        {
            return rhs * lhs;
        }

        public static TVector operator -(VectorBase<TScalar, TVector, TInvert> vector)
        {
            return vector.Create(i => vector.Operations.Negative(vector[i]));
        }

        public override bool Equals(object obj)
        {
            VectorBase<TScalar, TVector, TInvert> rhs = obj as VectorBase<TScalar, TVector, TInvert>;
            if (rhs == null)
            {
                return false;
            }

            for (int i = 0; i < this.Size; i++)
            {
                if (!this.Operations.Compare(this[i], rhs[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator ==(VectorBase<TScalar, TVector, TInvert> lhs, VectorBase<TScalar, TVector, TInvert> rhs)
        {
            if (System.Object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(VectorBase<TScalar, TVector, TInvert> lhs, VectorBase<TScalar, TVector, TInvert> rhs)
        {
            return !(lhs == rhs);
        }

        public override int GetHashCode()
        {
            return this.BuildVectorString(x => this.Operations.GetCanonicalString(x)).GetHashCode();
        }
    }
}
