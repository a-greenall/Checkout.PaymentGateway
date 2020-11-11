using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Checkout.PaymentGateway.Api.Application
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieves a payment for a given payment ID.
        /// </summary>
        /// <param name="id">The payment ID.</param>
        /// <returns>An action result of type <see cref="PaymentResponseDto"/>.</returns>
        /// <example>GET api/payments/C69AD723-F435-47DD-ADCB-4158EE519914</example>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PaymentResponseDto>> Get(Guid id)
        {
            var payment = await _mediator.Send(new GetPayment { PaymentId = id });

            if (payment is null)
                return NotFound();
            
            return payment;
        }

        /// <summary>
        /// Submits a payment request.
        /// </summary>
        /// <param name="requestDto">The payment request DTO object.</param>
        /// <returns>An action result of type <see cref="PaymentRequestResponseDto"/>.</returns>
        /// <example>POST api/payments</example>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PaymentRequestResponseDto>> Post([FromBody] PaymentRequestDto requestDto)
        {
            var response = await _mediator.Send(new SendPaymentRequest { PaymentRequest = requestDto });
            return response;
        }
    }
}
