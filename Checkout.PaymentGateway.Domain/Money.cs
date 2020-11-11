using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using Checkout.PaymentGateway.Extensions;

namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// Encapsulates both the currency the value of an amount of money.
    /// </summary>
    public class Money : ValueObject
    {
        /// <summary>
        /// A currency code.
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// An amount of money.
        /// </summary>
        public decimal Amount { get; }

        public Money(
            string currency,
            decimal amount)
        {
            // In a real-world application, you would want more extensive validation here e.g. whether the amount exceeds any payment limits.
            Currency = currency.IsCurrencyCode() ? currency : throw new ArgumentOutOfRangeException(nameof(currency));
            Amount = amount > 0 ? amount : throw new ArgumentOutOfRangeException(nameof(amount));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Currency;
            yield return Amount;
        }
    }
}
