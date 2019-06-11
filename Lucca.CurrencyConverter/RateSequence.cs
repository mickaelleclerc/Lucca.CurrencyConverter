namespace Lucca.CurrencyConverter
{
    using System.Collections.Generic;
    using System.Linq;

    public class RateSequence
    {
        public static readonly RateSequence Empty = new RateSequence(new List<Rate>());

        private readonly IReadOnlyCollection<Rate> rates;

        public RateSequence(IReadOnlyCollection<Rate> rates)
        {
            this.rates = rates;
        }

        public Amount Convert(Amount amount)
        {
            if (!this.rates.Any())
            {
                return Amount.Zero;
            }

            return this.rates.Aggregate(amount, (current, rate) => rate * current);
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", this.rates)}]";
        }
    }
}