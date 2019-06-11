namespace Lucca.CurrencyConverter.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class CurrencyShould
    {
        [Fact]
        public void BeValid_Given_ThreeUpperLetters()
        {
            Action action = () => new Currency("AAA");

            action.Should().NotThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("AA")]
        [InlineData("AAAA")]
        [InlineData("aAA")]
        [InlineData("aaA")]
        [InlineData("aaa")]
        [InlineData("999")]
        public void Throw_ArgumentException_Given_SomethingElseThanThreeUpperLetters(string currency)
        {
            AssertThrowException(currency);
        }

        private static void AssertThrowException(string value)
        {
            Action action = () => new Currency(value);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Currency {value} must have 3 uppers letters.");
        }
    }
}