using Core.UseCases.MapperInterface;
using Infrastructure.Logger;
using Infrastructure.Logging;
using Infrastructure.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dependency
{
    public static class InfrastructureDependency
    {
        public static void AddInfraDependency(this IServiceCollection services) 
        {
            services.AddScoped<IMapper, MapperLibrary>();
            services.AddScoped<ILogger, SerilogLogger>();

        }
    }
}
