using AutoMapper;
using Checkout.PaymentGateway.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Application.Queries
{
    /// <summary>
    /// Query used to retrieve a payment associated with a given ID.
    /// </summary>
    public class GetPayment : IRequest<PaymentResponseDto>
    {
        public Guid PaymentId { get; set; }
    }

    /// <summary>
    /// Handler for the <see cref="GetPayment"/> query.
    /// </summary>
    public class GetPaymentHandler : IRequestHandler<GetPayment, PaymentResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IPaymentContext _context;

        public GetPaymentHandler(
            IMapper mapper,
            IPaymentContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PaymentResponseDto> Handle(GetPayment request, CancellationToken cancellationToken)
        {
            // Retrieve the payment from the database
            var payment = await _context.GetAsync(request.PaymentId, cancellationToken);

            if (payment is null)
                return null;

            // Map to the response type
            return _mapper.Map<PaymentResponseDto>(payment);
        }
    }
}
