using AspNetCore.Authentication.ApiKey;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Checkout.PaymentGateway.Api.Infrastructure.Auth
{
    /// <summary>
    /// A model of an API key.
    /// </summary>
    public class ApiKey : IApiKey
    {
        public ApiKey(
            string key,
            string owner,
            List<Claim> claims = null)
        {
            Key = string.IsNullOrEmpty(key) ? throw new ArgumentNullException(nameof(key)) : key;
            OwnerName = string.IsNullOrEmpty(owner) ? throw new ArgumentNullException(nameof(owner)) : owner;
            Claims = claims ?? new List<Claim>();
        }

        /// <summary>
        /// A key value.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// The name of the API key owner.
        /// </summary>
        public string OwnerName { get; }

        /// <summary>
        /// The collection of claims associated with this API key.
        /// </summary>
        public IReadOnlyCollection<Claim> Claims { get; }
    }
}
