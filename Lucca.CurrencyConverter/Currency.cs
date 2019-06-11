namespace Lucca.CurrencyConverter
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A currency, in the most specific sense is money in any form
    /// when in use or circulation as a medium of exchange. 
    /// </summary>
    public class Currency : ValueObject<Currency>
    {
        private const string Pattern = "^[A-Z]{3}$";
        
        private readonly string value;

        public Currency(string value)
        {
            if (!Regex.IsMatch(value, Pattern))
            {
                throw new ArgumentException($"Currency {value} must have 3 uppers letters.");
            }
            
            this.value = value;
        }

        public override string ToString() => this.value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.value;
        }
    }
};