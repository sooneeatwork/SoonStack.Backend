using EMS.Shared.Persistance;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Configuration;

namespace EMS.Shared.DependencyInjection
{
    public static class SharedUseCasesModule
    {
        public static void AddSharedUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            // Register shared use cases dependencies here
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IDbConnection>(sp => new MySqlConnection(configuration.GetConnectionString("DefaultConnection")));
            // Add other services as needed
        }
    }
}
