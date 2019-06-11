namespace Lucca.CurrencyConverter.Application.Parsing
{
    public class Content
    {
        public Content(Conversion conversion, ExchangeRatesTable exchangeRates)
        {
            this.Conversion = conversion;
            this.ExchangeRates = exchangeRates;
        }

        public Conversion Conversion { get; }

        public ExchangeRatesTable ExchangeRates { get; }
    }
}