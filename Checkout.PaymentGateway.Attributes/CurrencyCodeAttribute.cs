using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Checkout.PaymentGateway.Attributes
{
    /// <summary>
    /// Validates whether a string is a valid currency code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CurrencyCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is string str))
                return false;

            // Just considering these three as examples for now.
            return new[] { "GBP", "USD", "EUR" }.Contains(str);
        }
    }
}
