using Core.Domain.DomainModel.ProductModel;
using Core.Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.Repository.ProductCategoryRepo
{
    public interface IProductCategoryRepository : IGenericRepository
    {
        Task<long> AddCategoryHierarchyAsync(Dictionary<string, object> hierarchyData);
        Task<IEnumerable<ProductCategory>> GetAllWithHierarchyAsync();
        Task<IEnumerable<ProductCategory>> GetChildCategoriesAsync(long parentCategoryId);
        Task<int> GetCountByCategoryNameAsync(string categoryName);
        Task<(ProductCategory parentCategory, ProductCategory childCategory)> GetParentAndChildCategoriesAsync(long parentCategoryId, long childCategoryId);
        Task<(ProductCategory parentCategory, ProductCategory categoryToMove)> GetParentCategoriesAsync(long v, long newParentId);
        Task<bool> MoveCategoryToNewParentAsync(ProductCategory categoryToMove, ProductCategory parentCategory);
    }
}
