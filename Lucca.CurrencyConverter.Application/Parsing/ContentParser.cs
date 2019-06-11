namespace Lucca.CurrencyConverter.Application.Parsing
{
    using System.IO;
    using Lucca.Shared.Functional;

    public class ContentParser
    {
        private readonly ConversionParser conversionParser;
        private readonly ExchangeRatesParser exchangeRatesParser;

        public ContentParser(
            ConversionParser conversionParser,
            ExchangeRatesParser exchangeRatesParser)
        {
            this.conversionParser = conversionParser;
            this.exchangeRatesParser = exchangeRatesParser;
        }

        public Result<Content> Parse(string content)
        {
            using (var contentReader = new StringReader(content))
            {
                var exchangeResult = this.ParseExchange(contentReader);

                if (exchangeResult.IsFailure)
                {
                    return Result.Failure<Content>(exchangeResult.ErrorMessage);
                }

                var exchangeRatesResult = this.ParseExchangeRates(contentReader);

                if (exchangeRatesResult.IsFailure)
                {
                    return Result.Failure<Content>(exchangeRatesResult.ErrorMessage);
                }

                return Result.Ok(new Content(exchangeResult.Value, exchangeRatesResult.Value));
            }
        }

        private Result<Conversion> ParseExchange(StringReader contentReader)
        {
            var exchangeLine = contentReader.ReadLine();

            return this.conversionParser.Parse(exchangeLine);
        }

        private Result<ExchangeRatesTable> ParseExchangeRates(StringReader contentReader)
        {
            var exchangeRatesLines = contentReader.ReadToEnd();

            return this.exchangeRatesParser.Parse(exchangeRatesLines);
        }
    }
}