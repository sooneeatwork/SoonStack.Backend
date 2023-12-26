namespace CustomerSlices.Repository.Repository
{
    public class CustomerRepository : GenericRepository,ICustomerRepository
    {
        
        public CustomerRepository(IDbConnection connection, IDbSqlExecutor dbExecutor) : base(connection, dbExecutor)
        {
        }

        public async Task<CustomersTable?> GetByEmailAsync(string newEmail)
        {
            var query = new Query(CustomersTable.TableName)
                           .Where(nameof(CustomersTable.Email), newEmail);

            Dictionary<string,object> parameter = new Dictionary<string, object>()
            {
                { "@p0",newEmail}
            };

           var result =  await _dbExecutor.ExecuteQueryAsync<CustomersTable>(parameter,query);

            return result;
        }

        public async Task<int> GetCountByEmailAsync(string newEmail)
        {
            int result = -1;

            var query = new Query(CustomersTable.TableName)
                              .Where(nameof(CustomersTable.Email), newEmail)
                              .AsCount();

            Dictionary<string, object> parameter = new Dictionary<string, object>()
            {
                { "@p0",newEmail}
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
