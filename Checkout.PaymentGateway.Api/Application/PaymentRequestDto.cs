using Checkout.PaymentGateway.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Represents a payment request.
    /// </summary>
    public class PaymentRequestDto
    {
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [Range(1, 12)]
        public int ExpiryMonth { get; set; }

        [Required]
        [Range(1, 99)]
        public int ExpiryYear { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "The CVV must be 3 characters in length.")]
        public string Cvv { get; set; }

        [Required]
        [CurrencyCode]
        public string Currency { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
