using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Api.Application.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(
            ILogger<ErrorController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("/error")]
        public ActionResult<ExceptionResponse> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = context?.Error;
            var path = context?.Path;

            // The relevant metadata for the exception.
            var metadata = new Dictionary<string, object>
            {
                {"API endpoint", path}
            };

            // Log the error
            _logger.LogError(exception, "An unhandled exception was encountered", metadata);

            // TODO further logging/application insights/email

            // Return response
            return new ExceptionResponse(exception, path);
        }
    }
}
