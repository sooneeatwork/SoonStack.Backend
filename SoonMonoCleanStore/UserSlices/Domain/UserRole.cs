namespace UserSlices.Domain
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; private set; }
        public int RoleId { get; private set; }

        public static UserRole CreateUserRoles(int userId, int roleId)
        {
            return new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
        }
    }
}
