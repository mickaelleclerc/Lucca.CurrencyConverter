namespace Lucca.CurrencyConverter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// A rate represents a factor of conversion from one currency to another.
    /// </summary>
    public class Rate : ValueObject<Rate>
    {
        private const int RoundPrecision = 4;
        
        private readonly decimal value;

        public Rate(decimal value)
        {
            if (value <= 0m)
            {
                throw new ArgumentException($"Rate {value} must be strictly positive.");
            }
            
            this.value = decimal.Round(value, RoundPrecision);
        }

        public Rate Inverted => new Rate(1.0m / this.value);

        public override string ToString() => this.value.ToString(CultureInfo.InvariantCulture);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.value;
        }

        public static Amount operator *(Rate rate, Amount amount)
        {
            return new Amount(rate.value * amount);
        }
    }
}