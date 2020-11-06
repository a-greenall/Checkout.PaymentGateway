using AutoFixture.Kernel;
using System;
using System.Reflection;

namespace Checkout.PaymentGateway.Tests.Customisations
{
    public class ConstructorArgumentRelay<TTarget, TValueType> : ISpecimenBuilder
    {
        private readonly string _paramName;
        private readonly TValueType _value;

        public ConstructorArgumentRelay(string paramName, TValueType value)
        {
            _paramName = paramName;
            _value = value;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (!(request is ParameterInfo parameter))
                return new NoSpecimen();

            var isCard = parameter.Name == "card";


            if (parameter.Member.DeclaringType != typeof(TTarget) ||
                parameter.Member.MemberType != MemberTypes.Constructor ||
                parameter.ParameterType != typeof(TValueType) ||
                parameter.Name != _paramName)
                return new NoSpecimen();

            return _value;
        }
    }
}
