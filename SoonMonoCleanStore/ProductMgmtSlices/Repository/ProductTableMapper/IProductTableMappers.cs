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
        (Dictionary<string, object> dataFields, Dictionary<string, object> whereClause) CreateMapForUpdate(Product product, ProductTable productData);
        object CreateMapForUpdateStockCount(List<Product> modifiedProductList);
        Dictionary<string, object> MapToTableForInsert(Product product);
        Product MapToDomain(ProductTable productTable);
    }
}
