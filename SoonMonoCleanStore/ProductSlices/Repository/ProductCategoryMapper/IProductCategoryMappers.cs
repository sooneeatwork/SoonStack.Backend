using SharedKernel.Domain.DomainModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSlices.Repository.ProductCategoryMapper
{
    public interface IProductCategoryMappers
    {
         Dictionary<string, object> CreateMapForInsert(ProductCategory product);
       
    }
}
