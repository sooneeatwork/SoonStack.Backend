using DapperPersistance.DatabaseQueryExecutor;
using DapperPersistance.DBDialectComplier;
using DapperPersistance.DBDialectComplier.MySQLDialect;
using DapperPersistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SharedKernel.Domain.RepoInterface;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPersistance.DepedencyManagement
{
    public static class DapperDepedencyManagement
    {
        public static void AddDapperDependency(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Register your GenericRepository
            services.AddScoped<IGenericRepository, GenericRepository>();
            // Register your DatabaseContext or IDbConnection factory
            services.AddScoped<IDbConnection>(provider =>
                new MySqlConnection(configuration.GetConnectionString("DefaultConnection")));

            
            
            services.AddScoped<IDbSqlExecutor, DbSqlExecutor>();
            services.AddScoped<ISqlDialectComplier, MySqlDialectComplier>();


        }
    }
}
