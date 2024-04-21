using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ValueObject
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        public Address(string street, string city, string state, string postalCode, string country)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));

            // Additional validations can be added here
        }

        // Override Equals, GetHashCode, and ToString as needed for value objects
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool Equals(object instance)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (instance is Address other)
            {
                return Street == other.Street &&
                       City == other.City &&
                       State == other.State &&
                       PostalCode == other.PostalCode &&
                       Country == other.Country;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, State, PostalCode, Country);
        }

        public static bool AreAddressesDifferent(Address address1, Address address2)
        {
            if (address1 == null || address2 == null)
                return address1 != address2;

            return address1.Country != address2.Country ||
                   address1.State != address2.State ||
                   address1.City != address2.City ||
                   address1.PostalCode != address2.PostalCode ||
                   address1.Street != address2.Street;
        }

        // In CreateMapForUpdate method


        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {PostalCode}, {Country}";
        }
    }

}
