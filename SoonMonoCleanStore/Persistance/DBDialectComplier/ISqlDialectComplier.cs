using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPersistance.DBDialectComplier
{
    public interface ISqlDialectComplier
    {
        string Compile(Query query);
    }
}
