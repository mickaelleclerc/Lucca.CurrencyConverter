namespace Lucca.CurrencyConverter.Application.Tests.Parsing
{
    using FluentAssertions;
    using Lucca.CurrencyConverter.Application.Parsing;
    using Xunit;

    public class ConversionParserShould
    {
        [Theory]
        [InlineData("AAA;1;BBB")]
        [InlineData("AAA;10;BBB")]
        [InlineData("AAA;100;BBB")]
        [InlineData("AAA;1000;BBB")]
        public void Return_Ok_Given_ExchangeIsWellFormed(string conversionLine)
        {
            AssertParseOk(conversionLine);
        }

        [Theory]
        [InlineData("AAA;1.2;BBB")]
        [InlineData("AAA;e;BBB")]
        [InlineData("AAA;.;BBB")]
        [InlineData("AAA;/;BBB")]
        public void Return_Failure_Given_AmountIsNotAnInteger(string conversionLine)
        {
            AssertParseFailure(conversionLine);
        }

        [Theory]
        [InlineData("AA;1;BBB")]
        [InlineData("AAAA;1;BBB")]
        [InlineData("aAA;1;BBB")]
        [InlineData("aaA;1;BBB")]
        [InlineData("aaa;1;BBB")]
        [InlineData("999;1;BBB")]
        public void Return_Failure_Given_FromCurrencyWithSomethingElseThanThreeUpperLetters(string conversionLine)
        {
            AssertParseFailure(conversionLine);
        }

        [Theory]
        [InlineData("AAA;1;BB")]
        [InlineData("AAA;1;BBBB")]
        [InlineData("AAA;1;bBB")]
        [InlineData("AAA;1;bbB")]
        [InlineData("AAA;1;bbb")]
        [InlineData("AAA;1;333")]
        public void Return_Failure_Given_ToCurrencyWithSomethingElseThanThreeUpperLetters(string conversionLine)
        {
            AssertParseFailure(conversionLine);
        }

        private static void AssertParseOk(string conversionLine)
        {
            var conversionResult = new ConversionParser().Parse(conversionLine);

            conversionResult.IsOk.Should().BeTrue();
        }

        private static void AssertParseFailure(string conversionLine)
        {
            var conversionResult = new ConversionParser().Parse(conversionLine);

             conversionResult.IsFailure.Should().BeTrue();
        }
    }
}