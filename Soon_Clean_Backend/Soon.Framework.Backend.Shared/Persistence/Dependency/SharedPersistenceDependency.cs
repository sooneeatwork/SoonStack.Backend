using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Soon.Framework.Backend.Shared.Persistence.LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soon.Framework.Backend.Shared.Persistence.Dependency
{
    public static class SharedPersistenceDependency
    {
        public static void AddSharedPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            
        }
    }
}
