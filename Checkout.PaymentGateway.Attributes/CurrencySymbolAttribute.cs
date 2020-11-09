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

            // Just considering these three as examples for now.
            return new[] { '£', '$', '€' }.Contains(@char);
        }
    }
}
