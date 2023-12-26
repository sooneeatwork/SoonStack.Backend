namespace ProductSlices.Repository.Repository
{
    public interface IProductRepository : IGenericRepository
    {
        Task<int> GetCountByProductNameAsync(string name);
        Task<int> GetStockCountByIdAsync(int productId);
    }
}
