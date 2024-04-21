using SharedKernel.Domain.ValueObject;

namespace SharedKernel.Domain.DomainModel.CustomerModel
{
    public class Customer : BaseEntity
    {
        // Customer-specific properties
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public Address? BillingAddress { get; private set; }

        // Factory method to create a new Customer instance
        public static Customer CreateCustomer(string name,
                                              string email,
                                              Address address,
                                              DateTime dateOfBirth)
        {
            CheckEmail(email);
            CheckAddress(address);
            CheckName(name);

            // Additional validation can be added here

            return new Customer
            {
                Name = name,
                Email = email,
                DateOfBirth = dateOfBirth,
                BillingAddress = address,
            };
        }

        // Methods to update customer properties can be added here
        // Example:


        public void UpdateCustomerInfo(string newName,
                                       DateTime newDateOfBirth,
                                       Address newBillingAddress,
                                       string newEmail)
        {

            CheckEmail(newEmail);
            CheckAddress(newBillingAddress);
            CheckName(newName);
            CheckDateOfBirth(newDateOfBirth);

            Email = newEmail;
            BillingAddress = newBillingAddress;
            Name = newName;
            DateOfBirth = newDateOfBirth;
        }

        public static bool IsCustomerExists(int count)
        {
            return count > 0;
        }

        private static void CheckEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
            {
                throw new ArgumentException("Email cannot be empty", nameof(newEmail));
            }
        }

        private static void CheckAddress(Address newAddress)
        {
            if (newAddress == null)
                throw new ArgumentException("Adrress is empty");
        }

        private static void CheckName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty");
        }

        private void CheckDateOfBirth(DateTime newDateOfBirth)
        {

            if (newDateOfBirth > DateTime.Now)
                throw new ArgumentException("Date of birth cannot be future date");
        }


        // Additional methods and logic can be added as per requirements
    }
}
