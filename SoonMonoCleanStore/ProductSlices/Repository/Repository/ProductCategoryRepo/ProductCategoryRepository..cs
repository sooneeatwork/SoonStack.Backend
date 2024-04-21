using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductSlices.Repository.Repository.ProductCategoryRepo
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
                              .Where(nameof(ProductCategoryTable.Name), categoryName)
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
