namespace Lucca.CurrencyConverter
{
    using System;
    using System.Collections.Generic;

    public class Amount : ValueObject<Amount>
    {
        public static readonly Amount Zero = new Amount(0m);

        private const int RoundPrecision = 4;
        
        private readonly decimal value;

        public Amount(decimal value)
        {
            if (value < 0m)
            {
                throw new ArgumentException($"Amount {value} must be positive.");
            }
            
            this.value = decimal.Round(value, RoundPrecision);
        }

        public int Rounded => (int)decimal.Round(this.value, 0);

        public override string ToString() => $"{this.value}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.value;
        }

        public static implicit operator decimal(Amount amount) => amount.value;
    }
}