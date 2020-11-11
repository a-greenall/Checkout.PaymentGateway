using Checkout.PaymentGateway.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Infrastructure
{
    /// <summary>
    /// Provides a context for interraction with the payment database collection.
    /// </summary>
    public interface IPaymentContext
    {
        /// <summary>
        /// Retrieve a payment with a given payment ID.
        /// </summary>
        /// <param name="paymentId">The payment ID.</param>
        /// <param name="token">The cancellation token.</param>
        Task<Payment> GetAsync(Guid paymentId, CancellationToken token = default);

        /// <summary>
        /// Insert a payment.
        /// </summary>
        /// <param name="payment">The payment to insert.</param>
        /// <param name="token">The cancellation token.</param>
        Task InsertAsync(Payment payment, CancellationToken token = default);
    }

    /// <inheritdoc />
    public class PaymentContext : IPaymentContext
    {
        private IMongoCollection<Payment> _payment;

        public PaymentContext(IOptions<PaymentDbSettings> settings)
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));

            var client = new MongoClient(settings.Value.ConnectionString);
            var db = client.GetDatabase(settings.Value.Database);
            _payment = db.GetCollection<Payment>("payment");
        }

        /// <inheritdoc />
        public Task<Payment> GetAsync(Guid paymentId, CancellationToken token = default)
        {
            return _payment.Find(p => p.Id.Equals(paymentId))
                .SingleOrDefaultAsync(token);
        }

        /// <inheritdoc />
        public Task InsertAsync(Payment payment, CancellationToken token = default)
        {
            return _payment.InsertOneAsync(payment, new InsertOneOptions { BypassDocumentValidation = false }, token);
        }
    }
}
