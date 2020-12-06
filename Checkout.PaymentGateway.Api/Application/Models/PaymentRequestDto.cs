using Checkout.PaymentGateway.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Represents a payment request.
    /// </summary>
    public class PaymentRequestDto
    {
        /// <summary>
        /// The payment card number.
        /// </summary>
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        /// <summary>
        /// The payment card expiry month.
        /// </summary>
        [Required]
        [Range(1, 12)]
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// The payment card expiry year.
        /// </summary>
        [Required]
        [Range(1, 99)]
        public int ExpiryYear { get; set; }

        /// <summary>
        /// The payment card CVV.
        /// </summary>
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CVV must be 3 characters in length.")]
        public string Cvv { get; set; }

        /// <summary>
        /// The payment currency code.
        /// </summary>
        [Required]
        [CurrencyCode]
        public string Currency { get; set; }

        /// <summary>
        /// The payment amount.
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
