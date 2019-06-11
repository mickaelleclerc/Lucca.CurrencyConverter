namespace Lucca.CurrencyConverter.PathSearch
{
    using System.Collections.Generic;
    using System.Linq;

    public class DijkstraPathFinder
    {
        private readonly List<Node> nodes = new List<Node>();

        public DijkstraPathFinder(ExchangeRatesTable table)
        {
            // We computed inverted exchange rates so that the graph will contains
            // all possible nodes connections.

            // For example we can't find a path between EUR -> JPY with these exchange rates :
            // EUR -> CHF
            // AUD -> CHF
            // AUD -> JPY
            // By inverting AUD -> CHF we provided the full path to EUR -> JPY.
            var exchangeRates = table.ExchangeRates
                .Concat(WithInvertedExchangeRates(table.ExchangeRates))
                .ToList();

            foreach (var exchangeRate in exchangeRates)
            {
                this.AddNodes(exchangeRate);
            }
        }

        public RateSequence FindSequence(Currency from, Currency to)
        {
            var unvisitedGraph = this.InitializeSearch(from);

            while (unvisitedGraph.HasSome)
            {
                // For the first run through, the node corresponding to
                // the «from» currency will always be the closest one.
                var closest = unvisitedGraph.GetNext();

                if (closest.IsMatch(to))
                {
                    return closest.RatesFromStart;
                }

                // If we ever pop off a node which is at infinite distance
                // from start it means all remaining nodes are inaccessible.
                // Then we know there is no path to the destination.
                if (closest.IsAtInfiniteFromStart)
                {
                    break;
                }

                Visit(closest);
            }

            return RateSequence.Empty;
        }

        private void AddNodes(ExchangeRate exchangeRate)
        {
            var fromNode = this.StoreNode(exchangeRate.From);
            var toNode = this.StoreNode(exchangeRate.To);

            fromNode.AddConnectionTo(new NodeConnection(toNode, exchangeRate.Rate));
        }

        private Node StoreNode(Currency currency)
        {
            var node = this.nodes.FirstOrDefault(v => v.IsMatch(currency));

            if (node == null)
            {
                node = new Node(currency);
                this.nodes.Add(node);
            }

            return node;
        }

        private UnvisitedGraph InitializeSearch(Currency from)
        {
            foreach (var node in this.nodes)
            {
                if (node.IsMatch(from))
                {
                    node.MarkAsSourceNode();
                }
            }

            return new UnvisitedGraph(this.nodes);
        }

        private static IReadOnlyCollection<ExchangeRate> WithInvertedExchangeRates(IReadOnlyCollection<ExchangeRate> exchangeRates)
        {
            return exchangeRates
                .Select(exchangeRate => exchangeRate.Inverted)
                .ToList();
        }

        private static void Visit(Node current)
        {
            foreach (var neighbour in current.Neighbours)
            {
                if (neighbour.IsFurtherAwayThan(current.DistanceFromStart.ToNeighbour))
                {
                    neighbour.ReachableFrom(current);
                }
            }
        }
    }
}