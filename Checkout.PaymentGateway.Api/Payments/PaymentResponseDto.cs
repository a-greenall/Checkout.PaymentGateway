using System;

namespace Checkout.PaymentGateway.Api.Payments
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
        public char CurrencySymbol { get; set; }
        public decimal Amount { get; set; }
    }
}
