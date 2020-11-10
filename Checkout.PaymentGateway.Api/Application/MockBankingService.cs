using System;
using System.Net;

namespace Checkout.PaymentGateway.Api.Application
{
    public interface IBankingService
    {
        /// <summary>
        /// Submits a payment request to the banking service.
        /// </summary>
        /// <param name="payment">The requested payment.</param>
        /// <returns>A response including the payment ID and status.</returns>
        public BankResponse<Guid> SubmitPayment(PaymentRequestDto payment);
    }

    /// <summary>
    /// A simulated instance of a banking service.
    /// </summary>
    public class MockBankingService : IBankingService
    {
        /// <inheritdoc />
        public BankResponse<Guid> SubmitPayment(PaymentRequestDto payment)
        {
            // This simulates a successful response.
            return new BankResponse<Guid>
            {
                Response = Guid.NewGuid(),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
