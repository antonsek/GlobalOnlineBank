using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Converter
    {
        private static readonly Dictionary<string, decimal> RatesToKzt = new()
        {
            { "USD", 525m },
            { "EUR", 607m },
            { "KZT", 1m } // базовая валюта
         };

        /// <summary>
        /// Конвертирует сумму из одной валюты в другую.
        /// </summary>
        public static decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (string.IsNullOrWhiteSpace(fromCurrency) || string.IsNullOrWhiteSpace(toCurrency))
                throw new ArgumentException("Currency codes must be provided");

            fromCurrency = fromCurrency.ToUpperInvariant();
            toCurrency = toCurrency.ToUpperInvariant();

            if (!RatesToKzt.ContainsKey(fromCurrency))
                throw new InvalidOperationException($"Unsupported source currency: {fromCurrency}");

            if (!RatesToKzt.ContainsKey(toCurrency))
                throw new InvalidOperationException($"Unsupported target currency: {toCurrency}");

            // Переводим в KZT
            decimal amountInKzt = amount * RatesToKzt[fromCurrency];

            // Переводим из KZT в целевую валюту
            decimal result = amountInKzt / RatesToKzt[toCurrency];

            return Math.Round(result, 2);
        }
    }
}
