using AutoFixture;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Tests.Customisations;
using FluentAssertions;
using System;
using Xunit;

namespace Checkout.PaymentGateway.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void Not_allow_non_currency_code()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Money, string>("currency", "OMG");

            Action act = () => fixture.Create<Money>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("currency");
        }

        [Fact]
        public void Not_allow_zero_amount()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Money, string>("currency", "USD");
            fixture.ConstructorArgumentFor<Money, decimal>("amount", 0);

            Action act = () => fixture.Create<Money>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("amount");
        }

        [Fact]
        public void Not_allow_amount_less_than_zero()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Money, string>("currency", "USD");
            fixture.ConstructorArgumentFor<Money, decimal>("amount", -999);

            Action act = () => fixture.Create<Money>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("amount");
        }
    }
}
