using ProductMgmtSlices.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.ProductTableMapper
{
    public interface IProductTableMappers
    {
        Dictionary<string, object> MapToTableForInsert(Product product);
    }
}
