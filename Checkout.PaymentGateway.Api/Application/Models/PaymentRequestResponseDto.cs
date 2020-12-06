using System;
using System.Net;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Represents a reponse from a payment request.
    /// </summary>
    public class PaymentRequestResponseDto
    {
        /// <summary>
        /// The payment ID.
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// The status code indication whether or not the payment was successful.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}
