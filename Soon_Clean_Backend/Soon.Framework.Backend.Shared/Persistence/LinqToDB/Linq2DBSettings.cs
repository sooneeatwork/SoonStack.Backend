using LinqToDB.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soon.Framework.Backend.Shared.Persistence.LinqToDB
{
    public class Linq2DBSettings : ILinqToDBSettings
    {
        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public readonly IConnectionStringSettings mConnectionStringSettings;
        public Linq2DBSettings(IConfiguration configuration)
        {
            // Figure out the database name from the connection string.
            var sDBConnection = configuration["ConnectionStrings:DefaultConnection"] ?? "";
            var sProviderName = configuration["Authentication:Linq2db:ProviderName"] ?? "";
            if (sProviderName.Length == 0)
            {
                sProviderName = DefaultDataProvider;
            }

            mConnectionStringSettings = new ConnectionStringSettings
            {
                Name = "DefaultConnection",
                ProviderName = sProviderName,
                ConnectionString = sDBConnection
            };
        }

        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return mConnectionStringSettings;
            }
        }
    }
}
