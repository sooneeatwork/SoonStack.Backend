using EMS.Shared.Persistance;


using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EMS.UseCases.TicketMgmt.Application.TicketModule.DependencyInjection
{
    public static class TicketModuleDependency
    {
        public static void AddTicketModule(this IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        }
    }
}
