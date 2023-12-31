namespace UserSlices.Domain
{
    
    public class Role : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public static Role CreateRole(string name, string description)
        {
            return new Role
            {
                Name = name,
                Description = description
            };
        }
    }
}
