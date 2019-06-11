namespace Lucca.CurrencyConverter.PathSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UnvisitedGraph
    {
        private readonly List<Node> nodes;

        public UnvisitedGraph(IReadOnlyCollection<Node> nodes)
        {
            this.nodes = nodes.ToList();
        }

        public bool HasSome => this.nodes.Any();

        public Node GetNext()
        {
            var closest = this.GetClosest();
            this.MarkAsVisited(closest);

            return closest;
        }

        private Node GetClosest()
        {
            if (!this.HasSome)
            {
                throw new InvalidOperationException("There is no more unvisited node in the graph.");
            }

            this.nodes.Sort((x, y) => x.DistanceFromStart - y.DistanceFromStart);

            return this.nodes.First();
        }

        private void MarkAsVisited(Node node)
        {
            this.nodes.Remove(node);
        }
    }
}