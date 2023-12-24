using ProductSlices.Repository.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSlices.Repository.ProductTableMapper
{
    public interface IProductTableMappers
    {
        Dictionary<string, object> CreateMapForUpdate(Product product);
        Dictionary<string, object> CreateMapForInsert(Product product);
        IEnumerable<Dictionary<string, object>> CreateMapForUpdateStockCount(List<Product> productList);

        (Dictionary<string, object> dataFields, 
         Dictionary<string, object> whereClause) CreateMapForUpdate(Product modifiedProduct, 
                                                                    ProductTable originalProduct);
    }
}
