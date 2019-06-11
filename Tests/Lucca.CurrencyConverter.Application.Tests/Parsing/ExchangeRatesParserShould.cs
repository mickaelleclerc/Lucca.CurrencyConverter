namespace Lucca.CurrencyConverter.Application.Tests.Parsing
{
    using System;
    using FluentAssertions;
    using Lucca.CurrencyConverter.Application.Parsing;
    using Xunit;

    public class ExchangeRatesParserShould
    {
        [Fact]
        public void Return_Success_Given_ContentIsWellFormed()
        {
            AssertParseOk("1, AAA;BBB;1.2345");
        }

        [Theory]
        [InlineData("e")]
        [InlineData(".")]
        [InlineData("/")]
        [InlineData("1.2")]
        public void Return_Failure_Given_ExchangeRatesCountIsNotAnInteger(string exchangeRatesLines)
        {
            AssertParseFailure(exchangeRatesLines);
        }

        [Theory]
        [InlineData("1, AA;BBB;1.2345")]
        [InlineData("1, AAAA;BBB;1.2345")]
        [InlineData("1, aAA;BBB;1.1234")]
        [InlineData("1, aaA;BBB;1.2345")]
        [InlineData("1, aaa;BBB;1.2345")]
        [InlineData("1, 999;BBB;1.2345")]
        public void Return_Failure_Given_ExchangeRateFromCurrencyWithSomethingElseThanThreeUpperLetters(string exchangeRatesLines)
        {
            AssertParseFailure(exchangeRatesLines);
        }

        [Theory]
        [InlineData("1, AAA;BB;1.2345")]
        [InlineData("1, AAA;BBBB;1.2345")]
        [InlineData("1, AAA;bBB;1.2345")]
        [InlineData("1, AAA;bbB;1.2345")]
        [InlineData("1, AAA;bbb;1.2345")]
        [InlineData("1, AAA;999;1.2345")]
        public void Return_Failure_Given_ExchangeRateToCurrencyWithSomethingElseThanThreeUpperLetters(string exchangeRatesLines)
        {
            AssertParseFailure(exchangeRatesLines);
        }

        [Theory]
        [InlineData("1, AAA;BBB;1")]
        [InlineData("1, AAA;BBB;1.123")]
        [InlineData("1, AAA;BBB;1.12345")]
        [InlineData("1, AAA;BBB;e")]
        public void Return_Failure_Given_RateIsNotDecimalNumberWithFourDecimals(string exchangeRatesLines)
        {
            AssertParseFailure(exchangeRatesLines);
        }

        private static void AssertParseOk(string exchangeRatesLines)
        {
            var exchangeRatesResult = new ExchangeRatesParser().Parse(exchangeRatesLines.Replace(", ", Environment.NewLine));

            exchangeRatesResult.IsOk.Should().BeTrue();
        }

        private static void AssertParseFailure(string exchangeRatesLines)
        {
            var exchangeRatesResult = new ExchangeRatesParser().Parse(exchangeRatesLines.Replace(", ", Environment.NewLine));

            exchangeRatesResult.IsFailure.Should().BeTrue();
        }
    }
}