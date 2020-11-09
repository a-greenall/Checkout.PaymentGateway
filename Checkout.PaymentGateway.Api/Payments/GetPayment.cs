using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Payments
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
        private IMapper _mapper;

        public GetPaymentHandler(
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<PaymentResponseDto> Handle(GetPayment request, CancellationToken cancellationToken)
        {
            // TODO - retrieve payment from database
            // TODO - map to DTO
            throw new NotImplementedException();
        }
    }
}
