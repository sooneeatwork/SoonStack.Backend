using Core.Domain.DomainModel.ProductModel;
using DapperPersistance.DatabaseQueryExecutor;
using DapperPersistence;
using SqlKata;
using System.Data;


namespace ProductMgmtSlices.Repository.Repository.ProductCategoryRepo
{
    public class ProductCategoryReposirory: GenericRepository,IProductCategoryRepository
    {
        public ProductCategoryReposirory(IDbConnection connection, IDbSqlExecutor dbExecutor) : base(connection, dbExecutor)
        {
        }

        public Task<long> AddCategoryHierarchyAsync(Dictionary<string, object> hierarchyData)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategory>> GetAllWithHierarchyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategory>> GetChildCategoriesAsync(long parentCategoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountByCategoryNameAsync(string categoryName)
        {
            int result;
            var query = new Query(ProductCategoryTable.TableName)
                              .Where(nameof(ProductCategoryTable.name), categoryName)
                              .AsCount();

            Dictionary<string, object> parameter = _dbExecutor.CreateParameterDictionary(categoryName);

            try
            {
                result = await _dbExecutor.ExecuteQueryAsync<int>(parameter, query);
            }
            catch { throw; }

            return result;
        }

        public Task<(ProductCategory parentCategory, ProductCategory childCategory)> GetParentAndChildCategoriesAsync(long parentCategoryId, long childCategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<(ProductCategory parentCategory, ProductCategory categoryToMove)> GetParentCategoriesAsync(long v, long newParentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveCategoryToNewParentAsync(ProductCategory categoryToMove, ProductCategory parentCategory)
        {
            throw new NotImplementedException();
        }
    }
}
