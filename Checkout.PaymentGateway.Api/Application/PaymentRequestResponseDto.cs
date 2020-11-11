using System;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Represents a reponse from a payment request.
    /// </summary>
    public class PaymentRequestResponseDto
    {
        public Guid PaymentId { get; set; }

        public bool SubmittedSuccessfully { get; set; }
    }
}
