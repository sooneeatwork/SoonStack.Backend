using DapperPersistence;

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
