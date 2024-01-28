namespace ProductSlices.Repository.Repository.ProductRepo
{
    public interface IProductRepository : IGenericRepository
    {
        Task<int> GetCountByProductNameAsync(string name);
        Task<int> GetStockCountByIdAsync(int productId);
    }
}
