using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Payments
{
    /// <summary>
    /// Command used to send a payment request.
    /// </summary>
    public class SendPaymentRequest : IRequest<bool>
    {
        public PaymentRequestDto PaymentRequest { get; set; }
    }

    /// <summary>
    /// Handler for the <see cref="SendPaymentRequest"/> command.
    /// </summary>
    public class SendPaymentHandler : IRequestHandler<SendPaymentRequest, bool>
    {
        private IBankingService _bankingService;

        public SendPaymentHandler(
            IBankingService bankingService)
        {
            _bankingService = bankingService ?? throw new ArgumentNullException(nameof(bankingService));
        }

        public Task<bool> Handle(SendPaymentRequest request, CancellationToken cancellationToken)
        {
            // Submit the payment to the banking service.
            var paymentSubmission = _bankingService.SubmitPayment(request.PaymentRequest);

            // TODO save the payment

            return Task.FromResult(paymentSubmission.StatusCode == HttpStatusCode.OK);
        }
    }
}
