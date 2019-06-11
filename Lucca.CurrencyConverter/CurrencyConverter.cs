namespace Lucca.CurrencyConverter
{
    using Lucca.CurrencyConverter.PathSearch;

    /// <summary>
    /// An
    /// </summary>
    public class CurrencyConverter
    {
        private readonly ExchangeRatesTable exchangeRates;

        public CurrencyConverter(ExchangeRatesTable exchangeRates)
        {
            this.exchangeRates = exchangeRates;
        }

        public Amount Convert(Amount amount, Currency from, Currency to)
        {
            var pathFinder = new DijkstraPathFinder(this.exchangeRates);

            return pathFinder
                .FindSequence(from, to)
                .Convert(amount);
        }
    }
}