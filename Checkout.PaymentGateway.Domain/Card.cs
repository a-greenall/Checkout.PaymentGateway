using Checkout.PaymentGateway.Domain.Common;
using Checkout.PaymentGateway.Extensions;
using System;
using System.Collections.Generic;

namespace Checkout.PaymentGateway.Domain
{
    /// <summary>
    /// Represents a debit or credit card used for a payment.
    /// </summary>
    /// <remarks>This is under the assumption we are dealing with UK transactions only.</remarks>
    public class Card : ValueObject
    {
        // TODO - encrypt number
        public string Number { get; }
        public int ExpiryMonth { get; }
        public int ExpiryYear { get; }
        public string Cvv { get; }

        public Card(
            string number,
            int expiryMonth,
            int expiryYear,
            string cvv)
        {
            // In a real-world application, you would want more extensive validation here e.g. has the card expired etc.
            Number = ValidateCardNumber(number);
            ExpiryMonth = ValidateExpiryMonth(expiryMonth);
            ExpiryYear = ValidateExpiryYear(expiryYear);
            Cvv = ValidateCvv(cvv);
        }

        private string ValidateCardNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException(nameof(number));

            if (!number.IsCreditCard())
                throw new ArgumentOutOfRangeException(nameof(number));

            return number;
        }

        private int ValidateExpiryMonth(int expiryMonth)
        {
            if (expiryMonth > 12 || expiryMonth < 1)
                throw new ArgumentOutOfRangeException(nameof(expiryMonth));

            return expiryMonth;
        }

        private int ValidateExpiryYear(int expiryYear)
        {
            if (expiryYear < 1 || expiryYear > 99)
                throw new ArgumentOutOfRangeException(nameof(expiryYear));

            return expiryYear;
        }

        private string ValidateCvv(string cvv)
        {
            if (string.IsNullOrEmpty(cvv))
                throw new ArgumentNullException(nameof(cvv));

            // Assuming that we are dealing with UK debit/credit cards for now
            if (cvv.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(cvv));

            return cvv;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number;
            yield return ExpiryMonth;
            yield return ExpiryYear;
            yield return Cvv;
        }
    }
}
