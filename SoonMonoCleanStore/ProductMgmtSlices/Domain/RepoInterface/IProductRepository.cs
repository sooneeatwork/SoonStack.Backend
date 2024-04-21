using Core.Domain.RepoInterface;

namespace ProductMgmtSlices.Domain.RepoInterface
{
    public interface IProductRepository : IGenericRepository
    {
        Task<int> GetCountByProductNameAsync(string name);
        Task<int> GetStockCountByIdAsync(int productId);
        Task<IEnumerable<ProductTable>?> SearchAsync(string productName, decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize);
    }
}
