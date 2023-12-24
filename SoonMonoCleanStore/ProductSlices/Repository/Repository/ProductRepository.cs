using Dapper;
using DapperPersistance.DatabaseQueryExecutor;
using DapperPersistence;
using MySql.Data.MySqlClient;
using ProductSlices.Repository.DatabaseModel;
using SharedKernel.Domain.RepoInterface;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductSlices.Repository.Repository
{
    public class ProductRepository : GenericRepository, IProductRepository
    {
        public ProductRepository(IDbConnection connection, IDbSqlExecutor dbExecutor) : base(connection, dbExecutor)
        {
        }

        public async Task<int> GetCountByProductNameAsync(string name)
        {
            int result = -1;

            var query = new Query(ProductTable.TableName)
                              .Where(nameof(ProductTable.Name), name)
                              .AsCount();

            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                { "@p0",name}
            };

            try
            {
                result = await _dbExecutor.ExecuteQueryAsync<int>(parameter, query);
            }
            catch { throw; }

            return result;
        }

        public async Task<int> GetStockCountByIdAsync(int productId)
        {
            int result = -1;

            var query = new Query(ProductTable.TableName)
                              .Where(nameof(ProductTable.Id), productId)
                              .Select(nameof(ProductTable.StockQuantity));
                            

            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                { "@p0",productId}
            };

            try
            {
                result = await _dbExecutor.ExecuteQueryAsync<int>(parameter, query);
            }
            catch { throw; }

            return result;
        }
    }

}