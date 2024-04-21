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

        
    }
}
