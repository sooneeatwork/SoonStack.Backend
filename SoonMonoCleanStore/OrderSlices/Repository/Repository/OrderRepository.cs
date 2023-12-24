using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperPersistance.DatabaseQueryExecutor;
using DapperPersistence;
using OrderSlices.Repository.DatabaseModel;
using OrderSlices.Repository.Repository.RepoInterfaces;
using SharedKernel.PersistanceShared;
using SqlKata;
using SqlKata.Compilers;

namespace OrderSlices.Repository.Repository
{
    public class OrderRepository : GenericRepository, IOrderRepository
    {
        public OrderRepository(IDbConnection connection, IDbSqlExecutor dbSqlExecutor) : base(connection, dbSqlExecutor)
        {
        }

        public async Task<IEnumerable<OrderDetailsView>> GetOrderDetailsByIdAsync(long orderId)
        {
            try
            {
                var query = new Query(DatabaseViewName.OrderDetailsView)
                                .Where(nameof(orderId), orderId);

                Dictionary<string, object> parameter = new Dictionary<string, object>()
                {
                    { "@p0",orderId}
                };

                var orderDetails = await _dbExecutor.ExecuteQueryToListAsync<OrderDetailsView>(parameter, query);

                return orderDetails;
            }
            catch
            {
                throw;
            }
        }


    }
}
