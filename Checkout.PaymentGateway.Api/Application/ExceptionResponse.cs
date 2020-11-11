using System;

namespace Checkout.PaymentGateway.Api.Application
{
    /// <summary>
    /// Provides details of an exception.
    /// </summary>
    public class ExceptionResponse
    {
        /// <summary>
        /// Exception type
        /// </summary>
        public string ExceptionType { get; }

        /// <summary>
        /// Enpoint where the error occured
        /// </summary>
        public string Endpoint { get; }

        /// <summary>
        /// Description of the error message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Error response constructor
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="path"></param>
        public ExceptionResponse(Exception ex, string path)
        {
            ExceptionType = ex.GetType().Name;
            Endpoint = path;
            Message = ex.Message;
        }
    }
}
