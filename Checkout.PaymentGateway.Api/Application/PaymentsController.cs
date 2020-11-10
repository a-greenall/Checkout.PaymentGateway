using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Checkout.PaymentGateway.Api.Application
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        // GET api/payments/5
        [HttpGet("{id}")]
        public int Get(int id)
        {
            return id;
        }

        // POST api/payments
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
