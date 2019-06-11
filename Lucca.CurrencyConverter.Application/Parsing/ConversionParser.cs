namespace Lucca.CurrencyConverter.Application.Parsing
{
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using Lucca.Shared.Functional;

    public class ConversionParser
    {
        private const string ConversionPattern = "^([A-Z]{3});(\\d+);([A-Z]{3})$";

        public Result<Conversion> Parse(string content)
        {
            using (var contentReader = new StringReader(content))
            {
                var conversionLine = contentReader.ReadLine() ?? string.Empty;

                var conversionMatching = Regex.Match(conversionLine, ConversionPattern);

                if (!conversionMatching.Success)
                {
                    return Result.Failure<Conversion>($"Exchange {conversionLine} is malformed.");
                }

                var from = new Currency(conversionMatching.Groups[1].Value);
                var amount = new Amount(decimal.Parse(conversionMatching.Groups[2].Value, CultureInfo.InvariantCulture));
                var to = new Currency(conversionMatching.Groups[3].Value);

                return Result.Ok(new Conversion(amount, from, to));
            }
        }
    }
}