using Dapper;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EMS.Shared.Persistance
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly IDbConnection _dbConnection;
        protected readonly Compiler _compiler;

        public BaseRepository(IDbConnection connection)
        {
            _dbConnection = connection ?? throw new ArgumentNullException(nameof(connection));
            _compiler = new MySqlCompiler(); // Use appropriate compiler for your database
        }

        protected Query NewQuery(string tableName)
        {
            return new Query(tableName);
        }

        public  async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(string tableName)
        {
            var query = NewQuery(tableName);
            var compiledQuery = _compiler.Compile(query);
            return await _dbConnection.QueryAsync<TEntity>(compiledQuery.Sql, compiledQuery.NamedBindings);
        }

        public  async Task<TEntity> GetByIdAsync<TEntity>(long id)
        {
            string tableName = typeof(TEntity).Name;
            var query = NewQuery(tableName).Where(nameof(id), id);
            var compiledQuery = _compiler.Compile(query);
            return await _dbConnection.QueryFirstOrDefaultAsync<TEntity>(compiledQuery.Sql, compiledQuery.NamedBindings);
        }

        public async Task<TEntity> GetByCodeAsync<TEntity>(string code)
        {

            ArgumentNullException.ThrowIfNull(code);

            string tableName = typeof(TEntity).Name;
            string formattedCode = code.Trim().ToUpper();
            var query = NewQuery(tableName).Where(nameof(code), formattedCode);
            var compiledQuery = _compiler.Compile(query);
            return await _dbConnection.QueryFirstOrDefaultAsync<TEntity>(compiledQuery.Sql, compiledQuery.NamedBindings);
        }


        public async Task<int> InsertAsync<TEntity>(TEntity entity, IDbTransaction dbTransaction)
        {
            string tableName = typeof(TEntity).Name;
            var query = NewQuery(tableName).AsInsert(entity);
            var compiledQuery = _compiler.Compile(query);
            return await _dbConnection.ExecuteAsync(compiledQuery.Sql, compiledQuery.NamedBindings, dbTransaction);
        }


        public async Task<long> SelectMaxIDAsync<TEntity>()
        {
            string tableName = typeof(TEntity).Name;
            var maxIdQuery = new Query(tableName).SelectRaw("MAX(Id) as MaxId");
            var compiledMaxIdQuery = _compiler.Compile(maxIdQuery);

            long maxId = await _dbConnection.QueryFirstOrDefaultAsync<long>(compiledMaxIdQuery.Sql, 
                                                                            compiledMaxIdQuery.NamedBindings);
            return maxId;
        }



        public virtual async Task<bool> UpdateFieldByIdAsync<TEntity>(Dictionary<string, object> fieldToUpdate,
                                                                      Dictionary<string, object> idFields,
                                                                      IDbTransaction dbTransaction)
        {
            if (fieldToUpdate == null || idFields == null)
                throw new ArgumentNullException("Both fieldToUpdate and idFields should be provided");

            string tableName = typeof(TEntity).Name;
            var query = NewQuery(tableName);

            query = query.AsUpdate(fieldToUpdate)
                         .Where(idFields);

            var compiledQuery = _compiler.Compile(query);
            return await _dbConnection.ExecuteAsync(compiledQuery.Sql,
                                                  compiledQuery.NamedBindings, dbTransaction) > 0;
        }


        public virtual async Task<int> DeleteAsync<TEntity>(string tableName, object id)
        {
            var query = NewQuery(tableName).Where("Id", id).AsDelete();
            var compiledQuery = _compiler.Compile(query);
            return await _dbConnection.ExecuteAsync(compiledQuery.Sql, compiledQuery.NamedBindings);
        }
    }
}
