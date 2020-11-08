using System.Net;

namespace Checkout.PaymentGateway.Api.Payments
{
    public class BankResponse<T>
    {
        public T Response { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
