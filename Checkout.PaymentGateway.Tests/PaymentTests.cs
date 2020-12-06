using AutoFixture;
using AutoFixture.Idioms;
using Checkout.PaymentGateway.Domain;
using Checkout.PaymentGateway.Tests.Customisations;
using FluentAssertions;
using System;
using Xunit;

namespace Checkout.PaymentGateway.Tests
{
    public class PaymentTests
    {
        [Fact]
        public void Not_allow_empty_constructor_parameter_values()
        {
            var fixture = new Fixture();
            var card = new Card("4916114233264815", 12, 20, "123");
            var amount = new Money("GBP", 123);
            fixture.Inject(card);
            fixture.Inject(amount);

            var assertion = new GuardClauseAssertion(fixture, new OutOfRangeBehaviourExpectation());

            assertion.Verify(typeof(Payment).GetConstructors());
        }
    }
}
