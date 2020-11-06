using AutoFixture;
using AutoFixture.Idioms;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Tests.Customisations;
using FluentAssertions;
using System;
using Xunit;

namespace Checkout.PaymentGateway.Tests
{
    public class CardTests
    {
        [Fact]
        public void Not_allow_empty_constructor_parameter_values()
        {
            var fixture = new Fixture();

            var assertion = new GuardClauseAssertion(fixture, new OutOfRangeBehaviourExpectation());

            assertion.Verify(typeof(Card).GetConstructors());
        }

        [Fact]
        public void Not_allow_number_length_greater_than_16()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "112233445566778899");

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("number");
        }

        [Fact]
        public void Not_allow_number_length_less_than_16()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "11223344556677");

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("number");
        }

        [Fact]
        public void Not_allow_expiry_month_greater_than_12()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "1122334455667788");
            fixture.ConstructorArgumentFor<Card, int>("expiryMonth", 13);

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("expiryMonth");
        }

        [Fact]
        public void Not_allow_expiry_month_less_than_1()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "1122334455667788");
            fixture.ConstructorArgumentFor<Card, int>("expiryMonth", 0);

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("expiryMonth");
        }

        [Fact]
        public void Not_allow_expiry_year_greater_than_99()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "1122334455667788");
            fixture.ConstructorArgumentFor<Card, int>("expiryMonth", 6);
            fixture.ConstructorArgumentFor<Card, int>("expiryYear", 100);

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("expiryYear");
        }

        [Fact]
        public void Not_allow_expiry_year_less_than_1()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "1122334455667788");
            fixture.ConstructorArgumentFor<Card, int>("expiryMonth", 6);
            fixture.ConstructorArgumentFor<Card, int>("expiryYear", 0);

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("expiryYear");
        }

        [Fact]
        public void Not_allow_cvv_length_less_than_3()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "1122334455667788");
            fixture.ConstructorArgumentFor<Card, int>("expiryMonth", 6);
            fixture.ConstructorArgumentFor<Card, int>("expiryYear", 20);
            fixture.ConstructorArgumentFor<Card, string>("cvv", "00");

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("cvv");
        }

        [Fact]
        public void Not_allow_cvv_length_greater_than_3()
        {
            var fixture = new Fixture();

            fixture.ConstructorArgumentFor<Card, string>("number", "1122334455667788");
            fixture.ConstructorArgumentFor<Card, int>("expiryMonth", 6);
            fixture.ConstructorArgumentFor<Card, int>("expiryYear", 20);
            fixture.ConstructorArgumentFor<Card, string>("cvv", "0000");

            Action act = () => fixture.Create<Card>();

            act.Should().Throw<Exception>()
                .WithInnerException<Exception>()
                .WithInnerException<ArgumentOutOfRangeException>()
                .And.ParamName.Should().Be("cvv");
        }
    }
}
