using SqlKata.Compilers;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPersistance.DBDialectComplier.PostgreSql
{
    public class PostgreSQLDialectComplier : ISqlDialectComplier
    {
        public string Compile(Query query)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            PostgresCompiler postgresCompiler = new PostgresCompiler();
            return postgresCompiler.Compile(query).Sql;
        }
    }

 
}
