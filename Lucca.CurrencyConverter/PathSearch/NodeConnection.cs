namespace Lucca.CurrencyConverter.PathSearch
{
    public class NodeConnection
    {
        public NodeConnection(Node to, Rate rate)
        {
            this.To = to;
            this.Rate = rate;
        }

        public Node To { get; }

        public Rate Rate { get; }
    }
}