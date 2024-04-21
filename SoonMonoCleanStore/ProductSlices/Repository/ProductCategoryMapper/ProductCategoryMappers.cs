using SharedKernel.Domain.DomainModel.ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSlices.Repository.ProductCategoryMapper
{
    public class ProductCategoryMappers : IProductCategoryMappers
    {


        public Dictionary<string, object> CreateMapForInsert(ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            var mapping = new Dictionary<string, object>
            {
                { nameof(productCategory.CategoryName), productCategory.CategoryName },
                { nameof(productCategory.CategoryDescription), productCategory.CategoryDescription },
                { nameof(productCategory.IsActive), productCategory.IsActive },
                { nameof(productCategory.ModifiedBy), productCategory.IsActive },
                { nameof(productCategory.ModifiedDate), productCategory.IsActive },
                { nameof(productCategory.CreatedBy), productCategory.CreatedBy },
                { nameof(productCategory.CreatedDate), productCategory.CreatedDate }
            };

            return mapping;
        }
    }
}
   
