using EMS.Core.TicketMgmt.DiscountStrategies;
using EMS.Core.TicketMgmt.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.TicketMgmt.Dependency
{
    public static class TicketDomainDependency
    {
        public static void AddTicketDomain(this IServiceCollection services)
        {
            // Register your dependencies here

            services.AddTransient<IDiscountService, DiscountService>();
            services.AddScoped<IDiscountStrategy, BulkBookingDiscountStrategy>();
            services.AddScoped<IDiscountStrategy, LoyaltyDiscountStrategy>();
        }
    }
}
