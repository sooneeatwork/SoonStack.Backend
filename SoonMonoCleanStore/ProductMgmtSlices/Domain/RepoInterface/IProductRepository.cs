using SharedKernel.Domain.RepoInterface;

namespace ProductMgmtSlices.Domain.RepoInterface
{
    public interface IProductRepository : IGenericRepository
    {
        Task<int> GetCountByProductNameAsync(string name);
    }
}
