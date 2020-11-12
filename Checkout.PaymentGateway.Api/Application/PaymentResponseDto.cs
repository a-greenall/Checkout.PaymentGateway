using System;
using System.Net;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Represents a payment response.
    /// </summary>
    public class PaymentResponseDto
    {
        /// <summary>
        /// The payment ID.
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// The payment card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// The payment card expiry month.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The payment card expiry year.
        /// </summary>
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The payment card CVV.
        /// </summary>
        public string Cvv { get; set; }

        /// <summary>
        /// The payment currency code.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The payment amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The status code indication whether or not the payment was successful.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}
