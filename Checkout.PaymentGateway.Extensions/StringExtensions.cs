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
    }
}
