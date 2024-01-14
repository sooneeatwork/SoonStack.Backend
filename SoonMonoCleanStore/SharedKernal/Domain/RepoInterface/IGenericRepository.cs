using System.Data;
using System.Data.Common;

namespace SharedKernel.Domain.RepoInterface
{
    public interface IGenericRepository
    {
        Task<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class;
        Task<IEnumerable<TEntity>?> GetByIdListAsync<TEntity>(List<long> idLists) where TEntity : class;


        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;

        Task<int> InsertOneAsync<TEntity>(Dictionary<string, object> data, IDbTransaction? transaction = null) where TEntity : class;
        Task<long> InsertOneGetIdAsync<TEntity>(Dictionary<string, object> data, IDbTransaction? transaction = null) where TEntity : class;

        Task<long> InsertOneGetIdPgAsync<TEntity>(Dictionary<string, object> data, IDbTransaction? transaction = null) where TEntity : class;

        Task<int> InsertManyAsync<TEntity>(IEnumerable<Dictionary<string, object>> data, IDbTransaction? transaction = null) where TEntity : class;

        Task<int> UpdateOneAsync<TEntity>(Dictionary<string, object> dataFields, Dictionary<string, object> whereClause, IDbTransaction? transaction = null) where TEntity : class;
        Task<int> ExecuteStoredProcedureAsync<TEntity>(string storedProcedureName,
                                              object parameters,
                                              IDbTransaction? transaction = null) where TEntity : class;

        Task<int> DeleteOneAsync<TEntity>(Dictionary<string, object> data, IDbTransaction? transaction = null) where TEntity : class;
        Task<int> DeleteManyAsync<TEntity>(IEnumerable<Dictionary<string, object>> data, IDbTransaction? transaction = null) where TEntity : class;

        Task<IEnumerable<T>> ExecuteReadQueryAsync<T>(string sql, IDbTransaction? transaction = null);
        // Method for executing non-query SQL operation (insert, update, delete)
        Task<int> ExecuteNonQueryAsync(string sql, object param, IDbTransaction? transaction = null);
    }
}
