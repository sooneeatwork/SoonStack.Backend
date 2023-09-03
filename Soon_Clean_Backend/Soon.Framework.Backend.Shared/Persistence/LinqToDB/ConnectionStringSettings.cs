using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soon.Framework.Backend.Shared.Persistence.LinqToDB
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
        public bool IsGlobal => false;
    }
}
