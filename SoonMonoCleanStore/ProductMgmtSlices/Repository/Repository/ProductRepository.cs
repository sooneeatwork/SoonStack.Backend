﻿using DapperPersistance.DatabaseQueryExecutor;
using DapperPersistence;
using SqlKata;
using System.Collections.Generic;
using System.Data;


namespace ProductMgmtSlices.Repository.Repository
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
                              .Where(nameof(ProductTable.name), name)
                              .AsCount();

            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                { _dbExecutor.GetParameterPlaceHolder(),name}
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
            int result;

            var query = new Query(ProductTable.TableName)
                              .Where(nameof(ProductTable.id), productId)
                              .Select(nameof(ProductTable.stock_quantity));


            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                { _dbExecutor.GetParameterPlaceHolder(), productId}
            };


            try
            {
                result = await _dbExecutor.ExecuteQueryAsync<int>(parameter, query);
            }
            catch { throw; }

            return result;
        }

        public async Task<IEnumerable<ProductTable>?> SearchAsync(string productName,
                                                                        decimal? minPrice,
                                                                        decimal? maxPrice,
                                                                        int pageNumber,
                                                                        int pageSize)
        {
            IEnumerable<ProductTable>? result;
            try
            {

                var query = new Query(ProductTable.TableName)
                              .WhereBetween(nameof(ProductTable.price), minPrice, maxPrice)
                              .WhereLike(nameof(ProductTable.name), productName);

                var parameters = _dbExecutor.CreateParameterDictionary(minPrice.GetValueOrDefault(),
                                                                        maxPrice.GetValueOrDefault(),
                                                                        productName);

                result = await _dbExecutor.ExecuteQueryToListAsync<ProductTable>(parameters, query);
            }
            catch { throw; }

            return result;
        }
    }
}
