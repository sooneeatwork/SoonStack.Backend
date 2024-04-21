using SqlKata.Compilers;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPersistance.DBDialectComplier.PostgreSqlDialect
{
   
    public class PostgreSqlDialectComplier : ISqlDialectComplier
    {
        public string Compile(Query query)
        {
            PostgresCompiler sqlCompiler = new PostgresCompiler();
            return sqlCompiler.Compile(query).Sql;
        }
    }
}
