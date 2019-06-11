namespace Lucca.CurrencyConverter.Tests
{
    using FluentAssertions;
    using Xunit;

    public class ExchangeRateShould
    {
        [Fact]
        public void Return_Inverted()
        {
            var exchangeRate = new ExchangeRate(
                new Currency("EUR"),
                new Currency("CHF"),
                new Rate(1.2053m));

            var expected = new ExchangeRate(
                new Currency("CHF"),
                new Currency("EUR"),
                new Rate(0.8297m));

            exchangeRate.Inverted.Should().Be(expected);
        }
    }
}