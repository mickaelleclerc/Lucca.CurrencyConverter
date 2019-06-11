namespace Lucca.CurrencyConverter.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class RateShould
    {
        [Fact]
        public void Throw_ArgumentException_Given_Negative()
        {
            Action action = () => new Rate(-1m);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("Rate -1 must be strictly positive.");
        }

        [Fact]
        public void Throw_ArgumentException_Given_Zero()
        {
            Action action = () => new Rate(0m);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage("Rate 0 must be strictly positive.");
        }
    }
}