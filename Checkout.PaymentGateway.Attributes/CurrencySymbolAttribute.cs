using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Checkout.PaymentGateway.Attributes
{
    public class CurrencySymbolAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is char @char))
                return false;

            return new[] { '£', '$', '€' }.Contains(@char);
        }
    }
}
