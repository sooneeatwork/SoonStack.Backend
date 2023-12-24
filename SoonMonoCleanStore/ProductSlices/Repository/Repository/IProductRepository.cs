using ProductSlices.Repository.DatabaseModel;
using SharedKernel.Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSlices.Repository.Repository
{
    public interface IProductRepository : IGenericRepository
    {
        Task<int> GetCountByProductNameAsync(string name);
        Task<int> GetStockCountByIdAsync(int productId);
    }
}
