using System;

namespace Checkout.PaymentGateway.Domain.Common
{
    public abstract class Entity
    {
        /// <summary>
        /// The ID of the entity.
        /// </summary>
        /// <remarks>This is a GUID for the purposes of this demo application as it is easy to generate.</remarks>
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType().Name != other.GetType().Name)
                return false;

            if (Id == null || other.Id == null)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().Name + Id).GetHashCode();
        }
    }
}
