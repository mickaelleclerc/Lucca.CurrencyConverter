namespace Lucca.CurrencyConverter
{
    using System.Collections.Generic;

    /// <summary>
    /// In finance, an exchange rate is the rate at which one currency will be exchanged for another.
    /// </summary>
    public class ExchangeRate : ValueObject<ExchangeRate>
    {
        public ExchangeRate(Currency from, Currency to, Rate rate)
        {
            this.From = from;
            this.To = to;
            this.Rate = rate;
        }
        
        public Currency From { get; }
        
        public Currency To { get; }

        public Rate Rate { get; }
        
        public ExchangeRate Inverted => new ExchangeRate(this.To, this.From, this.Rate.Inverted);

        public override string ToString()
        {
            return $"{this.From}:{this.To}:{this.Rate}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.From;
            yield return this.To;
            yield return this.Rate;
        }
    }
}