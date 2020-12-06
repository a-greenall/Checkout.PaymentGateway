using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoMapper;
using Checkout.PaymentGateway.Api.Application.Queries;
using Checkout.PaymentGateway.Api.Infrastructure;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Infrastructure;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.Tests
{
    public class GetPaymentTests
    {
        private const string FAKE_CREDIT_CARD_NO = "4916114233264815";

        [Fact]
        public void Not_allow_empty_constructor_parameter_values_in_handler()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(typeof(GetPaymentHandler).GetConstructors());
        }

        [Fact]
        public async Task Retrieve_payment_dto_with_provided_id()
        {
            var fixture = new Fixture();

            // Arrange the payment
            var paymentId = fixture.Freeze<Guid>();
            var card = new Card(FAKE_CREDIT_CARD_NO, 12, 20, "123");
            var amount = new Money("EUR", 123);
            var payment = new Payment(card, amount, paymentId);

            // Arrange the payment context
            var mockPaymentContext = new Mock<IPaymentContext>();
            mockPaymentContext.Setup(m => m.GetAsync(It.Is<Guid>(g => g == paymentId), It.IsAny<CancellationToken>())).Returns(Task.FromResult(payment));
            fixture.Inject(mockPaymentContext.Object);

            // Arrange the mapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentMappingProfile>());
            fixture.Inject(config.CreateMapper());

            // Arrange the handler and query
            var handler = fixture.Create<GetPaymentHandler>();
            var query = fixture.Create<GetPayment>();

            var result = await handler.Handle(query, default);

            result.PaymentId.Should().Be(paymentId);
        }

        [Fact]
        public async Task Return_null_if_payment_is_null()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Arrange the payment context
            var mockPaymentContext = new Mock<IPaymentContext>();
            mockPaymentContext.Setup(m => m.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Payment>(default));
            fixture.Inject(mockPaymentContext.Object);

            // Arrange the handler and query
            var handler = fixture.Create<GetPaymentHandler>();
            var query = fixture.Create<GetPayment>();

            var result = await handler.Handle(query, default);

            result.Should().BeNull();
        }
    }
}
