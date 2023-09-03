using LinqToDB.Data;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soon.Framework.Backend.Shared.Persistence.LinqToDB
{
    public partial class Linq2db : DataConnection
    {
        public Linq2db()
        {
            InitDataContext();
        }

        public Linq2db(string configuration)
            : base(configuration)
        {
            InitDataContext();
        }

        public Linq2db(DataOptions<Linq2db> options)
            : base(options.Options)
        {
            InitDataContext();
        }

        partial void InitDataContext();
    }
}
