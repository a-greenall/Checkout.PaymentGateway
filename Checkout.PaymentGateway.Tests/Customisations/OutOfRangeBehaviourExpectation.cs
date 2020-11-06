using AutoFixture.Idioms;
using System;
using System.Globalization;

namespace Checkout.PaymentGateway.Tests.Customisations
{
    public class OutOfRangeBehaviourExpectation : IBehaviorExpectation
    {
        public void Verify(IGuardClauseCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            if (!command.RequestedType.IsClass
                && !command.RequestedType.IsInterface)
                return;

            try
            {
                command.Execute(null);
            }
            catch (ArgumentNullException e)
            {
                if (string.Equals(e.ParamName, command.RequestedParameterName, StringComparison.Ordinal))
                    return;

                throw command.CreateException(
                    "<null>",
                    string.Format(CultureInfo.InvariantCulture,
                        "Guard Clause prevented it, however the thrown exception contains invalid parameter name. " +
                        "Ensure you pass correct parameter name to the ArgumentNullException constructor.{0}" +
                        "Expected parameter name: {1}{0}Actual parameter name: {2}",
                        Environment.NewLine,
                        command.RequestedParameterName,
                        e.ParamName),
                    e);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }

            throw command.CreateException("null");
        }
    }
}
