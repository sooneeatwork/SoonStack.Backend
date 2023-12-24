using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPersistance.DatabaseQueryExecutor
{
    public interface IDbSqlExecutor
    {
        Task<TEntity?> ExecuteQueryAsync<TEntity>(Dictionary<string, object> parameter, Query query);

        Task<IEnumerable<TEntity>> ExecuteQueryToListAsync<TEntity>(Dictionary<string, object> parameter, Query query);

        Task<int> ExecuteScalarAsync<TEntity>(Dictionary<string, object> parameter, Query query);
    }
}
