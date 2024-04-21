using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Domain.RepoInterface
{
    public interface IProductMediaRepository : IGenericRepository
    {
        Task AddProductMediaAsync(ProductMedia productMedia);
    }
}
