using AspNetCore.Authentication.ApiKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Infrastructure.Auth
{
    /// <summary>
    /// An in-memory repository of API keys for demo purposes.
    /// </summary>
    public class InMemoryApiKeyRepository : IApiKeyRepository
    {
        private IEnumerable<IApiKey> _cache = new List<IApiKey>
        {
            new ApiKey("demo123", "Admin")
        };

        /// <inheritdoc />
        public Task<IApiKey> GetApiKeyAsnc(string key)
        {
            // Simply retrieves the from the cache for now.
            var apiKey = _cache.FirstOrDefault(k => k.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(apiKey);
        }
    }
}
