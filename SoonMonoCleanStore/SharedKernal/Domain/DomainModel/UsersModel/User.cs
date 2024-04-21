namespace Core.Domain.DomainModel.UsersModel
{
    public class User : BaseEntity
    {
        public long Id { get; set; }

        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;


        public Address? Address { get; private set; } // Address is a value object



        public static User CreateUser(int id, string email, string password, string firstName, string lastName, DateTime createdDate, DateTime modifiedDate, int createdBy, int modifiedBy, Address address)
        {
            return new User
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Address = address
            };
        }
    }
}
