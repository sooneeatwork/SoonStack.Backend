using SharedKernel.Domain.RepoInterface;

namespace ProductMgmtSlices.Domain.RepoInterface
{
    public interface IProductRepository : IGenericRepository
    {
        Task GetCountByProductNameAsync(string name);
    }
}
