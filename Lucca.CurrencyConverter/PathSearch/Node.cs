namespace Lucca.CurrencyConverter.PathSearch
{
    using System.Collections.Generic;
    using System.Linq;

    public class Node
    {
        private readonly List<NodeConnection> connections = new List<NodeConnection>();
        private readonly List<NodeConnection> connectionsFromStart = new List<NodeConnection>();

        private readonly Currency currency;

        public Node(Currency currency)
        {
            this.currency = currency;
            this.DistanceFromStart = Distance.Infinite;
        }

        public IReadOnlyCollection<Node> Neighbours => this.connections.Select(connection => connection.To).ToList();

        public Distance DistanceFromStart { get; private set; }

        public bool IsAtInfiniteFromStart => this.DistanceFromStart.IsInfinite;

        public RateSequence RatesFromStart
        {
            get
            {
                return new RateSequence(this.connectionsFromStart
                    .Select(connection => connection.Rate)
                    .ToList());
            }
        }

        public void AddConnectionTo(NodeConnection connection) => this.connections.Add(connection);

        public bool IsMatch(Currency currency) => this.currency == currency;

        public void MarkAsSourceNode() => this.DistanceFromStart = Distance.Beginning;


        public bool IsFurtherAwayThan(Distance distance) => this.DistanceFromStart > distance;

        public void ReachableFrom(Node predecessor)
        {
            this.DistanceFromStart = predecessor.DistanceFromStart.ToNeighbour;

            this.connectionsFromStart.Clear();
            this.connectionsFromStart.AddRange(predecessor.connectionsFromStart);
            this.connectionsFromStart.Add(predecessor.connections.First(nodeConnection => nodeConnection.To == this));
        }
    }
}