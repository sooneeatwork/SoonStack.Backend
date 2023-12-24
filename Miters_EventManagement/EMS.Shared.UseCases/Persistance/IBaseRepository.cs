using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EMS.Shared.Persistance
{
    public interface IBaseRepository
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(string tableName);
        Task<TEntity> GetByIdAsync<TEntity>(long id);
        Task<TEntity> GetByCodeAsync<TEntity>(string code);
        Task<long> SelectMaxIDAsync<TEntity>();
        Task<int> InsertAsync<TEntity>(TEntity entity, IDbTransaction dbTransaction);
        Task<bool> UpdateFieldByIdAsync<TEntity>(Dictionary<string, object> fieldToUpdate,
                                                 Dictionary<string, object> idFields,
                                                 IDbTransaction dbTransaction);
        Task<int> DeleteAsync<TEntity>(string tableName, object id);
    }
}
