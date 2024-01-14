using SharedKernel.Domain.RepoInterface;
using System.Data;
using Dapper;
using DapperPersistance.DatabaseQueryExecutor;

namespace DapperPersistence
{
    public class GenericRepository : IGenericRepository
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbSqlExecutor _dbExecutor;
        public GenericRepository(IDbConnection connection, IDbSqlExecutor dbSqlExecutor)
        {
            _connection = connection;
            _dbExecutor = dbSqlExecutor;
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class 
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var sql = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { Id = id });
        }

        public async Task<IEnumerable<TEntity>?> GetByIdListAsync<TEntity>(List<long> idLists) where TEntity : class
        {
            if (idLists == null || !idLists.Any())
                return Enumerable.Empty<TEntity>(); // Return an empty collection if the list is null or empty

            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var sql = $"SELECT * FROM {tableName} WHERE Id IN @Ids";

            return await _connection.QueryAsync<TEntity>(sql, new { Ids = idLists });
        }

      

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var sql = $"SELECT * FROM {tableName}";
            return await _connection.QueryAsync<TEntity>(sql);
        }

        public async Task<int> InsertOneAsync<TEntity>(Dictionary<string, object> data, IDbTransaction? transaction = null) where TEntity : class
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Keys.Select(k => "@" + k));
            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            return await _connection.ExecuteAsync(sql, data, transaction);
        }

        public async Task<long> InsertOneGetIdAsync<TEntity>(Dictionary<string, object> data, IDbTransaction? transaction = null) where TEntity : class
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Keys.Select(k => "@" + k));
            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT LAST_INSERT_ID();";
            return await _connection.ExecuteScalarAsync<long>(sql, data, transaction);
        }

        public async Task<long> InsertOneGetIdPgAsync<TEntity>(Dictionary<string, object> data, IDbTransaction? transaction = null) where TEntity : class
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Keys.Select(k => "@" + k));
            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values}) RETURNING id;";

            return await _connection.ExecuteScalarAsync<long>(sql, data, transaction);
        }

        public async Task<int> InsertManyAsync<TEntity>(IEnumerable<Dictionary<string, object>> dataList, IDbTransaction? transaction = null) where TEntity : class
        {
           
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var totalCount = 0;
            foreach (var data in dataList)
            {
                var columns = string.Join(", ", data.Keys);
                var values = string.Join(", ", data.Keys.Select(k => "@" + k));
                var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                totalCount += await _connection.ExecuteAsync(sql, data, transaction);
            }
            return totalCount;
        }

        public async Task<int> UpdateOneAsync<TEntity>(Dictionary<string, object> data,
                                                       Dictionary<string, object> whereClause,
                                                       IDbTransaction? transaction = null) where TEntity : class
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var setClause = string.Join(", ", data.Keys.Select(k => $"{k} = @{k}"));
            var whereClauseSql = string.Join(", ", whereClause.Keys.Select(p => $"{p} = @{p}"));
            var sql = $"UPDATE {tableName} SET {setClause} WHERE {whereClauseSql}";
            var parameterDict = data.Concat(whereClause)
                                    .ToDictionary(pair => pair.Key, pair => pair.Value);

            return await _connection.ExecuteAsync(sql, parameterDict, transaction);
        }

     

        public async Task<int> 
         ExecuteStoredProcedureAsync<TEntity>(string storedProcedureName,
                                              object parameters,
                                              IDbTransaction? transaction = null) where TEntity : class
        {
            return await _connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                transaction,
                commandType: CommandType.StoredProcedure);
        }


        public async Task<int> DeleteOneAsync<TEntity>(Dictionary<string, object> data, 
                                                       IDbTransaction? transaction = null) where TEntity : class
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var sql = $"DELETE FROM {tableName} WHERE Id = @Id";
            return await _connection.ExecuteAsync(sql, data, transaction);
        }

        public async Task<int> DeleteManyAsync<TEntity>(IEnumerable<Dictionary<string, object>> dataList, 
                                                        IDbTransaction? transaction = null) where TEntity : class
        {
            var tableName = DatabaseUtil.GetTableName<TEntity>();
            var totalCount = 0;
            foreach (var data in dataList)
            {
                var sql = $"DELETE FROM {tableName} WHERE Id = @Id";
                totalCount += await _connection.ExecuteAsync(sql, data, transaction);
            }
            return totalCount;
        }

        public async Task<IEnumerable<T>> ExecuteReadQueryAsync<T>(string sql, IDbTransaction? transaction = null)
        {
            return await _connection.QueryAsync<T>(sql, transaction: transaction);
        }

        // Method for executing non-query SQL operation (insert, update, delete)
        public async Task<int> ExecuteNonQueryAsync(string sql, object param, IDbTransaction? transaction = null)
        {
            return await _connection.ExecuteAsync(sql, param, transaction);
        }

      
    }
}
