using AutoMapper;
using Checkout.PaymentGateway.Api.Infrastructure;
using Checkout.PaymentGateway.Api.Payments;
using Checkout.PaymentGateway.Domain;
using FluentAssertions;
using Xunit;

namespace Checkout.PaymentGateway.Tests
{
    public class AutoMapperTests
    {
        [Fact]
        public void Have_valid_mapping_configuration()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentMappingProfile>());
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Mapping_between_payment_request_and_payment_is_correct()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentMappingProfile>());
            var mapper = config.CreateMapper();
            var dto = new PaymentRequestDto
            {
                Amount = 999,
                CardNumber = "1122334455667788",
                CurrencySymbol = '£',
                Cvv = "123",
                ExpiryMonth = 6,
                ExpiryYear = 25
            };

            var payment = mapper.Map<PaymentRequestDto, Payment>(dto);

            payment.Amount.Amount.Should().Be(dto.Amount);
            payment.Amount.CurrencySymbol.Should().Be(dto.CurrencySymbol);
            payment.Card.Cvv.Should().Be(dto.Cvv);
            payment.Card.ExpiryMonth.Should().Be(dto.ExpiryMonth);
            payment.Card.ExpiryYear.Should().Be(dto.ExpiryYear);
            payment.Card.Number.Should().Be(dto.CardNumber);
        }

        [Fact]
        public void Mapping_between_payment_and_payment_response_is_correct()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentMappingProfile>());
            var mapper = config.CreateMapper();
            var payment = new Payment(
                new Card("1122334455667788", 6, 25, "123"),
                new Money('£', 123));

            var dto = mapper.Map<Payment, PaymentResponseDto>(payment);

            dto.Amount.Should().Be(payment.Amount.Amount);
            dto.CardNumber.Should().Be(payment.Card.Number);
            dto.CurrencySymbol.Should().Be(payment.Amount.CurrencySymbol);
            dto.Cvv.Should().Be(payment.Card.Cvv);
            dto.ExpiryMonth.Should().Be(payment.Card.ExpiryMonth);
            dto.ExpiryYear.Should().Be(payment.Card.ExpiryYear);
            dto.PaymentId.Should().Be(payment.Id);
        }
    }
}
