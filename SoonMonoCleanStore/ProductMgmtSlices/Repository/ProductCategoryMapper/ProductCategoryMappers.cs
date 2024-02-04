using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSlices.Repository.ProductCategoryMapper
{
    public class ProductCategoryMappers : IProductCategoryTableMappers
    {


        public Dictionary<string, object> CreateMapForInsert(ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException(nameof(productCategory));
            }

            var mapping = new Dictionary<string, object>
            {
                { nameof(ProductCategoryTable.id), productCategory.CategoryName },
                { nameof(ProductCategoryTable.name), productCategory.CategoryName },
                { nameof(ProductCategoryTable.category_description), productCategory.CategoryDescription },
                { nameof(ProductCategoryTable.is_active), productCategory.IsActive },
                { nameof(ProductCategoryTable.modified_by), productCategory.IsActive },
                { nameof(ProductCategoryTable.modified_date), productCategory.IsActive },
                { nameof(ProductCategoryTable.created_by), productCategory.CreatedBy },
                { nameof(ProductCategoryTable.created_date), productCategory.CreatedDate }
            };

            return mapping;
        }

        public (Dictionary<string, object> dataFields, Dictionary<string, object> whereClause) CreateMapForUpdate(ProductCategory productCategory, ProductCategoryTable productCategoryData)
        {
            throw new NotImplementedException();
        }

        public ProductCategory MapToDomain(ProductCategoryTable productCategoryData)
        {
            throw new NotImplementedException();
        }
    }
}
   
