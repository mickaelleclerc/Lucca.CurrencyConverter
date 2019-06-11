namespace Lucca.CurrencyConverter.PathSearch
{
    using System.Collections.Generic;

    public class Distance : ValueObject<Distance>
    {
        public static readonly Distance Beginning = new Distance(0);
        public static readonly Distance Infinite = new Distance(int.MaxValue);

        private const int DistanceBetweenTwoNeighbours = 1;

        private readonly int value;

        private Distance(int value)
        {
            this.value = value;
        }

        public bool IsInfinite => this == Infinite;

        public Distance ToNeighbour => new Distance(this + DistanceBetweenTwoNeighbours);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.value;
        }

        public static implicit operator int(Distance distance) => distance.value;
    }
}