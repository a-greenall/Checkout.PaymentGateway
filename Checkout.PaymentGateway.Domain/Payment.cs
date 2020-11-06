using Checkout.PaymentGateway.Domain.Common;
using System;

namespace Checkout.PaymentGateway.Domain
{
    public class Payment : Entity
    {
        public Card Card { get; }
        
        public Money Amount { get; }

        public Payment(
            Card card,
            Money amount)
        {
            Card = card ?? throw new ArgumentNullException(nameof(card));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }
    }
}
