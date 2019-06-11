namespace Lucca.CurrencyConverter.Application.Parsing
{
    public class Conversion
    {
        public Conversion(Amount amount, Currency from, Currency to)
        {
            this.From = from;
            this.To = to;
            this.Amount = amount;
        }

        public Amount Amount { get; }

        public Currency From { get; }

        public Currency To { get; }
    }
}