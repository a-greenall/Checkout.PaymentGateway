using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoMapper;
using Checkout.PaymentGateway.Api.Application;
using Checkout.PaymentGateway.Api.Infrastructure;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Infrastructure;
using FluentAssertions;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.Tests
{
    public class SendPaymentRequestTests
    {
        [Fact]
        public void Not_allow_empty_constructor_parameter_values_in_handler()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(typeof(SendPaymentRequestHandler).GetConstructors());
        }

        [Fact]
        public async Task Insert_payment_into_database()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Arrange the payment request DTO
            var dto = new PaymentRequestDto
            {
                Amount = 999,
                CardNumber = "1122334455667788",
                Currency = "GBP",
                Cvv = "123",
                ExpiryMonth = 6,
                ExpiryYear = 25
            };
            fixture.Inject(dto);

            var paymentId = fixture.Freeze<Guid>();

            // Arrange the banking service
            var mockBankingService = new Mock<IBankingService>();
            mockBankingService.Setup(m => m.SubmitPayment(It.Is<PaymentRequestDto>(p => p == dto)))
                .Returns(new BankResponse<Guid> { Response = paymentId, StatusCode = HttpStatusCode.OK });
            fixture.Inject(mockBankingService);

            // Arrange the mapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentMappingProfile>());
            fixture.Inject(config.CreateMapper());

            // Arrange the payment context
            var mockPaymentContext = new Mock<IPaymentContext>();
            fixture.Inject(mockPaymentContext.Object);

            // Arrange the handler and query
            var handler = fixture.Create<SendPaymentRequestHandler>();
            var command = fixture.Create<SendPaymentRequest>();

            var result = await handler.Handle(command, default);

            mockPaymentContext.Verify(c => c.InsertAsync(It.Is<Payment>(p => p.Id == paymentId), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Return_successful_response_if_bank_service_submission_successful()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Arrange the payment request DTO
            var dto = new PaymentRequestDto
            {
                Amount = 999,
                CardNumber = "1122334455667788",
                Currency = "USD",
                Cvv = "123",
                ExpiryMonth = 6,
                ExpiryYear = 25
            };
            fixture.Inject(dto);

            var paymentId = fixture.Freeze<Guid>();

            // Arrange the banking service
            var mockBankingService = new Mock<IBankingService>();
            mockBankingService.Setup(m => m.SubmitPayment(It.Is<PaymentRequestDto>(p => p == dto)))
                .Returns(new BankResponse<Guid> { Response = paymentId, StatusCode = HttpStatusCode.OK });
            fixture.Inject(mockBankingService);

            // Arrange the mapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentMappingProfile>());
            fixture.Inject(config.CreateMapper());

            // Arrange the handler and query
            var handler = fixture.Create<SendPaymentRequestHandler>();
            var command = fixture.Create<SendPaymentRequest>();

            var result = await handler.Handle(command, default);

            result.SubmittedSuccessfully.Should().BeTrue();
            result.PaymentId.Should().Be(paymentId);
        }

        [Fact]
        public async Task Return_unsuccessful_respone_if_bank_service_submission_unsuccessful()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Arrange the payment request DTO
            var dto = new PaymentRequestDto
            {
                Amount = 999,
                CardNumber = "1122334455667788",
                Currency = "EUR",
                Cvv = "123",
                ExpiryMonth = 6,
                ExpiryYear = 25
            };
            fixture.Inject(dto);

            var paymentId = fixture.Freeze<Guid>();

            // Arrange the banking service
            var mockBankingService = new Mock<IBankingService>();
            mockBankingService.Setup(m => m.SubmitPayment(It.Is<PaymentRequestDto>(p => p == dto)))
                .Returns(new BankResponse<Guid> { Response = paymentId, StatusCode = HttpStatusCode.InternalServerError });
            fixture.Inject(mockBankingService);

            // Arrange the mapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentMappingProfile>());
            fixture.Inject(config.CreateMapper());

            // Arrange the handler and query
            var handler = fixture.Create<SendPaymentRequestHandler>();
            var command = fixture.Create<SendPaymentRequest>();

            var result = await handler.Handle(command, default);

            result.SubmittedSuccessfully.Should().BeFalse();
            result.PaymentId.Should().Be(paymentId);
        }
    }
}
