namespace Lucca.CurrencyConverter.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using FluentAssertions;
    using Xunit;

    public class RateSequenceShould
    {
        [Fact]
        public void Return_Zero_Given_NoRates()
        {
            this.AssertExchange("", 1m, 0m);
        }

        [Fact]
        public void Return_InitialAmount_Given_RateIsOne()
        {
            this.AssertExchange("1.0", 0.9661m, 0.9661m);
        }

        [Fact]
        public void Return_InitialAmount_Given_AllRatesAreOne()
        {
            this.AssertExchange("1", 0.9661m, 0.9661m);
        }

        [Fact]
        public void Return_AmountEqualsToRate_Given_AmountIsOne()
        {
            this.AssertExchange("0.9661", 1m, 0.9661m);
        }

        [Fact]
        public void Return_ConvertedAmount()
        {
            this.AssertExchange("1.2053, 1.0351, 86.0305", 550m, 59032.6924m);
        }

        private void AssertExchange(string rateSequenceDescription, decimal initialAmount, decimal expectedAmount)
        {
            var sequence = this.MakeRateSequence(rateSequenceDescription);
            var convertedAmount = sequence.Convert(new Amount(initialAmount));

            convertedAmount.Should().Be(new Amount(expectedAmount));
        }

        private RateSequence MakeRateSequence(string exchangeRatesDescription)
        {
            var rates = new List<Rate>();

            if (exchangeRatesDescription == string.Empty)
            {
                return new RateSequence(rates);
            }

            var edgesDescriptions = exchangeRatesDescription.Split(", ");

            const string EdgeDescriptionPattern = "(\\d+(\\.\\d{4})?)";

            foreach (var edgeDescription in edgesDescriptions)
            {
                var match = Regex.Match(edgeDescription, EdgeDescriptionPattern);

                if (!match.Success)
                {
                    throw new ArgumentException($"Cannot parse description {exchangeRatesDescription}");
                }

                var rate = new Rate(decimal.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));

                rates.Add(rate);
            }

            return new RateSequence(rates);
        }
    }
}