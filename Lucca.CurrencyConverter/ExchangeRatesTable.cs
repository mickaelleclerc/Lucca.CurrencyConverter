namespace Lucca.CurrencyConverter
{
    using System.Collections.Generic;
    using System.Linq;

    public class ExchangeRatesTable
    {
        private readonly IReadOnlyCollection<ExchangeRate> exchangeRates;

        public ExchangeRatesTable(IReadOnlyCollection<ExchangeRate> exchangeRates)
        {
            this.exchangeRates = exchangeRates;
        }

        public IReadOnlyCollection<ExchangeRate> ExchangeRates => this.exchangeRates.ToList();

        public override string ToString()
        {
            return $"[{string.Join(", ", this.exchangeRates)}]";
        }
    }
}