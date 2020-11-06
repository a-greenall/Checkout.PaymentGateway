using System.Linq;

namespace Checkout.PaymentGateway.Extensions
{
    public static class CharExtensions
    {
        public static bool IsCurrencySymbol(this char @char)
        {
            // Only considering 3 currency symbols for demo purposes.
            return new[] { '£', '$', '€' }.Contains(@char);
        }
    }
}
