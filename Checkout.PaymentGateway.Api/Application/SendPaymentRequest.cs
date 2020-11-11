using AutoMapper;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Infrastructure;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Command used to send a payment request.
    /// </summary>
    public class SendPaymentRequest : IRequest<PaymentRequestResponseDto>
    {
        public PaymentRequestDto PaymentRequest { get; set; }
    }

    /// <summary>
    /// Handler for the <see cref="SendPaymentRequest"/> command.
    /// </summary>
    public class SendPaymentRequestHandler : IRequestHandler<SendPaymentRequest, PaymentRequestResponseDto>
    {
        private readonly IBankingService _bankingService;
        private readonly IMapper _mapper;
        private readonly IPaymentContext _context;

        public SendPaymentRequestHandler(
            IBankingService bankingService,
            IMapper mapper,
            IPaymentContext context)
        {
            _bankingService = bankingService ?? throw new ArgumentNullException(nameof(bankingService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PaymentRequestResponseDto> Handle(SendPaymentRequest request, CancellationToken cancellationToken)
        {
            // Submit the payment to the banking service.
            var submission = _bankingService.SubmitPayment(request.PaymentRequest);

            // Map the request to the entity type
            var payment = _mapper.Map<Payment>(request.PaymentRequest);
            payment.Id = submission.Response;

            // Insert the payment into to database
            await _context.InsertAsync(payment, cancellationToken);

            // Return the reponse
            return new PaymentRequestResponseDto
            {
                PaymentId = payment.Id,
                SubmittedSuccessfully = submission.StatusCode == HttpStatusCode.OK
            };
        }
    }
}
