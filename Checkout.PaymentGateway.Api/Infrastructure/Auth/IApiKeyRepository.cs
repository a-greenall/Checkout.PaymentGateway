using AspNetCore.Authentication.ApiKey;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Infrastructure.Auth
{
    /// <summary>
    /// Provides methods for operating on API keys.
    /// </summary>
    public interface IApiKeyRepository
    {
        /// <summary>
        /// Retrieves an <see cref="IApiKey"/> instance for a given key.
        /// </summary>
        /// <param name="key">An API key.</param>
        Task<IApiKey> GetApiKeyAsnc(string key);
    }
}
