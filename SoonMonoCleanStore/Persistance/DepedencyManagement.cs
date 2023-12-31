﻿using Microsoft.Extensions.DependencyInjection;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using SharedKernel.Domain.RepoInterface;
using System.Configuration;
using DapperPersistance.DatabaseQueryExecutor;

namespace DapperPersistence
{
    public static class DependencyManagement
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your DatabaseContext or IDbConnection factory
            services.AddScoped<IDbConnection>(provider =>
                new MySqlConnection(configuration.GetConnectionString("DefaultConnection")));

            // Register your GenericRepository
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IDbSqlExecutor, DbSqlExecutor>();
        }
    }
}
