using System.Linq;

namespace Checkout.PaymentGateway.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Masks a portion of the current string.
        /// </summary>
        /// <param name="str">The string to mask.</param>
        /// <param name="index">The index identifying the first character where the masking should begin.</param>
        /// <param name="maskChar">The character to use for the mask.</param>
        /// <returns>A masked string, or the original string if an invalid index is provided.</returns>
        public static string Mask(this string str, int index, char maskChar)
        {
            if (index < 0 || index >= str.Length)
                return str;

            var unmaskedStr = str.Substring(0, index);
            var maskedCharCount = str.Length - index;
            var maskedStr = string.Concat(Enumerable.Repeat(maskChar, maskedCharCount));

            return unmaskedStr + maskedStr;
        }

        /// <summary>
        /// Checks whether the provided string is a valid currency code.
        /// </summary>
        /// <param name="str">The string to check.</param>
        public static bool IsCurrencyCode(this string str)
        {
            // Only considering 3 currency codes for demo purposes.
            return new[] { "GBP", "USD", "EUR" }.Contains(str);
        }

        /// <summary>
        /// Checkes whether a string is a valid credit card number.
        /// </summary>
        /// <param name="str">The string to check.</param>
        /// <remarks>Adapted from https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/CreditCardAttribute.cs,dc8ba6a16f759ddb</remarks>
        public static bool IsCreditCard(this string str)
        {
            if (!(str is string ccValue))
                return false;
            
            ccValue = ccValue.Replace("-", "");
            ccValue = ccValue.Replace(" ", "");

            if (ccValue.Length != 16)
                return false;

            int checksum = 0;
            bool evenDigit = false;

            // http://www.beachnet.com/~hstiles/cardtype.html
            foreach (char digit in ccValue.Reverse())
            {
                if (digit < '0' || digit > '9')
                    return false;

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }
    }
}
