namespace Lucca.CurrencyConverter.Application
{
    using System;
    using System.IO;
    using System.Linq;
    using Lucca.CurrencyConverter;
    using Lucca.CurrencyConverter.Application.Parsing;
    using Lucca.Shared.Functional.Extensions;

    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("A full or relative file path must be provided");
            }

            var filePath = args.First();

            var fileContent = ReadFileContent(filePath);
            var contentResult = BuildContentParser().Parse(fileContent);

            return contentResult
                .Match(
                    failure: errorMessage => throw new ArgumentException(errorMessage),
                    ok: MakeConversion);
        }

        private static string ReadFileContent(string filePath)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }

        private static ContentParser BuildContentParser()
        {
            return new ContentParser(
                new ConversionParser(),
                new ExchangeRatesParser());
        }

        private static int MakeConversion(Content content)
        {
            var currencyConverter = new CurrencyConverter(content.ExchangeRates);

            var convertedAmount = currencyConverter.Convert(
                content.Conversion.Amount,
                content.Conversion.From,
                content.Conversion.To);

            return convertedAmount.Rounded;
        }
    }
}