using SharedKernel.Domain.DomainModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSlices.Repository.ProductCategoryMapper
{
    public interface IProductCategoryTableMappers
    {
         Dictionary<string, object> CreateMapForInsert(ProductCategory product);
        (Dictionary<string, object> dataFields, Dictionary<string, object> whereClause) CreateMapForUpdate(ProductCategory productCategory, ProductCategoryTable productCategoryData);
        ProductCategory MapToDomain(ProductCategoryTable productCategoryData);
    }
}
