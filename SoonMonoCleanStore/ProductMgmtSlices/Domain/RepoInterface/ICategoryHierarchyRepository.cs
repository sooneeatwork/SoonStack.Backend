using ProductMgmtSlices.UseCases.ProductCategoryUseCases.Queries;
using System.Data;

namespace ProductMgmtSlices.Domain.RepoInterface
{
    public interface ICategoryHierarchyRepository : IGenericRepository
    {
        Task<(bool parentExists, bool childExists)> CheckCategoriesExistAsync(long parentCategoryId, 
                                                                              long childCategoryId);
        Task<long> AddChildToParentAsync(long parentCategoryId, long childCategoryId, System.Data.IDbTransaction? transaction = null);
        Task<bool> RemoveChildFromParentAsync(long childCategoryId, long? parentCategoryId, System.Data.IDbTransaction? transaction = null);
        Task<bool> CheckParentChildRelationAsync(long value, long categoryId);
        Task<IEnumerable<CategoryHierarchyDto>> GetFullCategoryHierarchyAsync();
        Task<(CategoryHierarchy currentHierarchy, CategoryHierarchy newParentCategory)> GetHierarchyByIdAsync(long categoryId);
        Task<bool> UpdateHierarchyAsync(Dictionary<string, object> hierarchyData);
        Task<(bool parentExists, bool categoryToMoveExists)> CheckCategoryExistsAsync(long newParentId, long categoryId);
        Task<bool> MoveCategoryAndUpdateHierarchyAsync(long categoryId, long newParentId, IDbTransaction transaction);
        Task<bool> MoveCategoryAndUpdateHierarchyAsync(long categoryId, long newParentId);
    }
}
