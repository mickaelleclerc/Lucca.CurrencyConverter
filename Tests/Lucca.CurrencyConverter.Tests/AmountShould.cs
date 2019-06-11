namespace Lucca.CurrencyConverter.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class AmountShould
    {
        [Fact]
        public void BeValid_Given_Positive()
        {
            Action action = () => new Amount(1m);

            action.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void BeValid_Given_Zero()
        {
            Action action = () => new Amount(0m);

            action.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void Throw_ArgumentException_Given_Negative()
        {
            Action action = () => new Amount(-1m);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("Amount -1 must be positive.");
        }
    }
}