﻿using Checkout.PaymentGateway.Api.Application.Commands;
using Checkout.PaymentGateway.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Checkout.PaymentGateway.Api.Application.Controllers
{
    /// <summary>
    /// The payments controller.
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/payments")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructs a PaymentsController instance.
        /// </summary>
        /// <param name="mediator">A mediator for request/response.</param>
        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieves a specific payment based upon its unique payment ID.
        /// </summary>
        /// <param name="id">A payment ID.</param>
        /// <returns>An action result of type <see cref="PaymentResponseDto"/>.</returns>
        /// <example>GET api/payments/C69AD723-F435-47DD-ADCB-4158EE519914</example>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <param name="requestDto">A payment request object.</param>
        /// <returns>An action result of type <see cref="PaymentRequestResponseDto"/>.</returns>
        /// <example>POST api/payments</example>
        [HttpPost]
        public async Task<ActionResult<PaymentRequestResponseDto>> Post([FromBody] PaymentRequestDto requestDto)
        {
            return await _mediator.Send(new SendPaymentRequest { PaymentRequest = requestDto });
        }
    }
}
