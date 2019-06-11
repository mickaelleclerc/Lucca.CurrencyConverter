namespace Lucca.CurrencyConverter.Tests
{
    using FluentAssertions;
    using Lucca.CurrencyConverter.Tests.Helpers;
    using Xunit;

    public class CurrencyConverterShould
    {
        [Fact]
        public void Return_ConvertedAmount()
        {
            this.AssertConversion("EUR", "JPY", 550m, 59032.6924m);
            this.AssertConversion("USD", "INR", 550m, 29864.7196m);
            this.AssertConversion("KWU", "AUD", 550m, 0.4862m);
            this.AssertConversion("EUR", "SGD", 550m, 0m);
        }

        private void AssertConversion(string from, string to, decimal initialAmount, decimal expectedAmount)
        {
            var table = ExchangeRateTableCreator.From("AUD:CHF:0.9661, JPY:KWU:13.1151, EUR:CHF:1.2053, AUD:JPY:86.0305, EUR:USD:1.2989, JPY:INR:0.6571");
            var converter = new Lucca.CurrencyConverter.CurrencyConverter(table);

            var convertedAmount = converter.Convert(new Amount(initialAmount), new Currency(from), new Currency(to));

            convertedAmount.Should().Be(new Amount(expectedAmount));
        }
    }
}