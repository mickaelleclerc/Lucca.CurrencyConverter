namespace Lucca.CurrencyConverter.Application.Parsing
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Lucca.Shared.Functional;
    using Lucca.Shared.Functional.Extensions;

    public class ExchangeRatesParser
    {
        private const string ExchangeRatePattern = "^([A-Z]{3});([A-Z]{3});(\\d+\\.\\d{4})$";
        private const string ExchangeRateCountPattern = "^(\\d)+$";

        public Result<ExchangeRatesTable> Parse(string content)
        {
            using (var contentReader = new StringReader(content))
            {
                var exchangeRatesCountLine = contentReader.ReadLine() ?? string.Empty;

                var exchangeRatesCountMatching = Regex.Match(exchangeRatesCountLine, ExchangeRateCountPattern);

                if (!exchangeRatesCountMatching.Success)
                {
                    return Result.Failure<ExchangeRatesTable>($"Exchange rates count {exchangeRatesCountLine} is malformed.");
                }

                var exchangeRatesCount = int.Parse(exchangeRatesCountLine);

                var exchangeRatesResults = Enumerable
                    .Range(0, exchangeRatesCount)
                    .Select(_ => contentReader.ReadLine())
                    .Select(ParseExchangeRateLine)
                    .ToList();

                if (exchangeRatesResults.Any(result => result.IsFailure))
                {
                    return Result.Failure<ExchangeRatesTable>(exchangeRatesResults.JoinErrorMessages());
                }

                return Result.Ok(new ExchangeRatesTable(exchangeRatesResults.JoinValues()));
            }
        }

        private static Result<ExchangeRate> ParseExchangeRateLine(string exchangeRateLine)
        {
            var exchangeRateMatching = Regex.Match(exchangeRateLine, ExchangeRatePattern);

            if (!exchangeRateMatching.Success)
            {
                return Result.Failure<ExchangeRate>($"Exchange rate {exchangeRateLine} is malformed.");
            }

            var from = new Currency(exchangeRateMatching.Groups[1].Value);
            var to = new Currency(exchangeRateMatching.Groups[2].Value);
            var rate = new Rate(decimal.Parse(exchangeRateMatching.Groups[3].Value, CultureInfo.InvariantCulture));
                    
            return Result.Ok(new ExchangeRate(from, to, rate));
        }
    }
}