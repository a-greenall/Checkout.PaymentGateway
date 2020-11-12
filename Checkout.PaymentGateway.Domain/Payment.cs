using Checkout.PaymentGateway.Domain.Common;
using System;
using System.Net;

namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// Represents a payment.
    /// </summary>
    public class Payment : Entity
    {
        /// <summary>
        /// The card associated with the payment.
        /// </summary>
        public Card Card { get; }
        
        /// <summary>
        /// The payment amount.
        /// </summary>
        public Money Amount { get; }

        /// <summary>
        /// The status code indicating whether the payment was successful.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public Payment(
            Card card,
            Money amount,
            Guid id = default)
        {
            Card = card ?? throw new ArgumentNullException(nameof(card));
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            Id = id;
        }

        /// <summary>
        /// Updates the payment after receiving response from the bank service.
        /// </summary>
        /// <param name="paymentId">The received payment ID.</param>
        /// <param name="statusCode">The received status code.</param>
        public void UpdateFromBankResponse(Guid paymentId, HttpStatusCode statusCode)
        {
            Id = paymentId;
            StatusCode = statusCode;
        }
    }
}
