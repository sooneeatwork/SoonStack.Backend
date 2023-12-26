namespace CustomerSlices.Repository.RepoInterfaces
{
    public interface ICustomerRepository : IGenericRepository
    {
        Task<CustomersTable?> GetByEmailAsync(string newEmail);
        Task<int> GetCountByEmailAsync(string newEmail);
    }
}
