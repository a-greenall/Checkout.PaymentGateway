using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Collections.Generic;
using Checkout.PaymentGateway.Extensions;

namespace Checkout.PaymentGateway.Domain
{
    public class Money : ValueObject
    {
        public char CurrencySymbol { get; }
        public decimal Amount { get; }

        public Money(
            char currencySymbol,
            decimal amount)
        {
            // In a real-world application, you would want more extensive validation here e.g. whether the amount exceeds any payment limits.
            CurrencySymbol = currencySymbol.IsCurrencySymbol() ? currencySymbol : throw new ArgumentOutOfRangeException(nameof(currencySymbol));
            Amount = amount > 0 ? amount : throw new ArgumentOutOfRangeException(nameof(amount));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CurrencySymbol;
            yield return Amount;
        }
    }
}
