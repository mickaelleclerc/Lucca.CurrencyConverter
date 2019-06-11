namespace Lucca.CurrencyConverter.Tests.PathSearch
{
    using FluentAssertions;
    using Lucca.CurrencyConverter.PathSearch;
    using Lucca.CurrencyConverter.Tests.Helpers;
    using Xunit;

    public class DijkstraPathFinderShould
    {
        [Fact]
        public void Return_EmptyPath_Given_NoExchangeRates()
        {
            this.AssertShortestPath("", "[]");
        }

        [Fact]
        public void Return_EmptyPath_Given_NeitherFromNorToCurrencies()
        {
            this.AssertShortestPath("AUD:CHF:1", "[]");
        }

        [Fact]
        public void Return_EmptyPath_Given_NoFromCurrency()
        {
            this.AssertShortestPath("AUD:JPY:1", "[]");
        }

        [Fact]
        public void Return_EmptyPath_Given_NoToCurrency()
        {
            this.AssertShortestPath("EUR:CHF:1", "[]");
        }

        [Fact]
        public void Return_Path_Given_OneExchangeRate()
        {
            this.AssertShortestPath("EUR:JPY:1", "[1]");
        }

        [Fact]
        public void Return_Path_Given_TwoExchangeRates()
        {
            this.AssertShortestPath("EUR:CHF:1, CHF:JPY:2", "[1, 2]");
        }

        [Fact]
        public void Return_Path_Given_ThreeExchangeRates()
        {
            this.AssertShortestPath("EUR:CHF:1, CHF:AUD:2, AUD:JPY:3", "[1, 2, 3]");
        }

        [Fact]
        public void Return_Shortest_Path_Given_ParallelsPaths()
        {
            this.AssertShortestPath("EUR:CHF:1, CHF:AUD:2, AUD:JPY:3, EUR:JPY:10", "[10]");
            this.AssertShortestPath("EUR:CHF:1, CHF:USD:2, USD:JPY:3, EUR:AUD:10, AUD:JPY:11", "[10, 11]");
        }

        [Fact]
        public void Return_Path_Given_IndirectPath()
        {
            this.AssertShortestPath("EUR:CHF:1, AUD:CHF:2, AUD:JPY:3", "[1, 0.5, 3]");
        }

        private void AssertShortestPath(string exchangeRatesDescription, string expectedRateSequence)
        {
            var table = ExchangeRateTableCreator.From(exchangeRatesDescription);

            var pathFinder = new DijkstraPathFinder(table);
            var sequence = pathFinder.FindSequence(new Currency("EUR"), new Currency("JPY"));

            sequence.ToString().Should().Be(expectedRateSequence);
        }
    }
}