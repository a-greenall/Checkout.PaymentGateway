using System;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Represents a payment response.
    /// </summary>
    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvv { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public bool SubmittedSuccessfully { get; set; }
    }
}
