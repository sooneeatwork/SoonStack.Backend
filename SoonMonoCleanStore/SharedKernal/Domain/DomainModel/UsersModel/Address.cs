namespace SharedKernel.Domain.DomainModel.UsersModel
{
    public class Address : BaseEntity
    {
        public long Id { get; set; }
        public string AddressLine1 { get; private set; } = string.Empty;
        public string AddressLine2 { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;

        public static Address CreateAddress(string addressLine1,
                                            string addressLine2,
                                            string city,
                                            string state,
                                            string postalCode,
                                            string country)
        {
            return new Address
            {
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                City = city,
                State = state,
                PostalCode = postalCode,
                Country = country
            };
        }
    }
}
