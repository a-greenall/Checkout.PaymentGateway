using AspNetCore.Authentication.ApiKey;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Infrastructure.Auth
{
    /// <summary>
    /// A service that provides a way to retrieve an <see cref="IApiKey"/>.
    /// </summary>
    public class ApiKeyProvider : IApiKeyProvider
    {
        private readonly ILogger<ApiKeyProvider> _logger;
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyProvider(
            ILogger<ApiKeyProvider> logger,
            IApiKeyRepository apiKeyRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apiKeyRepository = apiKeyRepository ?? throw new ArgumentNullException(nameof(apiKeyRepository));
        }

        /// <summary>
        /// Provides an <see cref="IApiKey"/> with a given key value.
        /// </summary>
        /// <param name="key">The API key value.</param>
        public Task<IApiKey> ProvideAsync(string key)
        {
            try
            {
                return _apiKeyRepository.GetApiKeyAsnc(key);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}
