namespace Lucca.CurrencyConverter.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class ExchangeRateTableCreator
    {
        public static ExchangeRatesTable From(string exchangeRatesDescription)
        {
            var exchangeRates = new List<ExchangeRate>();

            var edgesDescriptions = exchangeRatesDescription.Split(", ");

            const string EdgeDescriptionPattern = "([A-Z]{3}):([A-Z]{3}):(\\d+(\\.\\d{4})?)";

            foreach (var edgeDescription in edgesDescriptions)
            {
                var match = Regex.Match(edgeDescription, EdgeDescriptionPattern);

                if (match.Success)
                {
                    var from = new Currency(match.Groups[1].Value);
                    var to = new Currency(match.Groups[2].Value);
                    var rate = new Rate(decimal.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture));

                    exchangeRates.Add(new ExchangeRate(from, to, rate));
                }
            }

            return new ExchangeRatesTable(exchangeRates);
        }
    }
}