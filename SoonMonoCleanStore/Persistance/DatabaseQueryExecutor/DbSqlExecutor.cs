using Dapper;
using DapperPersistance.DBDialectComplier;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DapperPersistance.DatabaseQueryExecutor
{
    public class DbSqlExecutor : IDbSqlExecutor
    {
        private readonly IDbConnection _connection;
        private readonly ISqlDialectComplier _compiler;

        public DbSqlExecutor(IDbConnection connection, ISqlDialectComplier compiler)
        {
            _connection = connection;
            _compiler = compiler;
        }

        public async Task<TEntity?> ExecuteQueryAsync<TEntity>(Dictionary<string, object> parameter, Query query)
        {
            var sql = _compiler.Compile(query);
            return await _connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameter);
        }

     

        public async Task<IEnumerable<TEntity>> ExecuteQueryToListAsync<TEntity>(Dictionary<string, object> parameter, Query query)
        {
            var sql = _compiler.Compile(query);
            return await _connection.QueryAsync<TEntity>(sql, parameter);
        }


        public async Task<int> ExecuteScalarAsync<TEntity>(Dictionary<string, object> parameter, Query query)
        {
            int result = -1;

            try
            {
                var sql = _compiler.Compile(query);
                
                result =  await _connection.ExecuteScalarAsync<int>(sql, parameter);
            }
            catch { throw; }

            return result;

        }

        public string GetParameterPlaceHolder()
        {
            return "@p@";
        }
    }
}
