using ProductMgmtSlices.Domain;
using SharedKernel.Domain.DomainModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.ProductTableMapper
{
    public interface IProductTableMappers
    {
        Dictionary<string, object> CreateMapForInsert(Product product);
        (Dictionary<string, object> dataFields, Dictionary<string, object> whereClause) CreateMapForUpdate(Product product, ProductTable productData);
        Product CreateMapForUpdateStockCount(List<Product> modifiedProductList);
        Product MapToDomain(ProductTable productData);
        Dictionary<string, object> MapToTableForInsert(Product product);
    }
}
