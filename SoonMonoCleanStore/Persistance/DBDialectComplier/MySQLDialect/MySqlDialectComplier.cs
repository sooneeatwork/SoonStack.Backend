using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPersistance.DBDialectComplier.MySQLDialect
{
    public class MySqlDialectComplier : ISqlDialectComplier
    {
        public string Compile(Query query)
        {
            MySqlCompiler mySqlCompiler = new MySqlCompiler();
            return mySqlCompiler.Compile(query).Sql;
        }
    }
}
