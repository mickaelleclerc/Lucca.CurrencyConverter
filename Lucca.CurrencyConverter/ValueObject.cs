namespace Lucca.CurrencyConverter
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A ValueObject represents a descriptive aspect of the domain with no conceptual identity.
    /// Value Objects are instantiated to represent elements of the design that we care about
    /// only for what they are, not who or which they are.
    /// (Domain-Driven Design: Tackling Complexity in the Heart of Software (2003), Eric Evans) 
    /// </summary>
    /// <remarks>
    /// A Value
    /// </remarks>
    /// <typeparam name="T">The class type that inherits form this ValueObject class.</typeparam>
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        /// <summary>
        /// The equality components are all properties/fields you want to take into account
        /// for the structural comparison.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            if (ReferenceEquals(valueObject, null))
            {
                return false;
            }

            return this.EqualsCore(valueObject);
        }

        private bool EqualsCore(ValueObject<T> other)
        {
            return this
                .GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return this
                .GetEqualityComponents()
                .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}