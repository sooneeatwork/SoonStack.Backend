using EMS.Modules.PromoMgmt.Domain.DomainServices;
using EMS.Modules.PromoMgmt.Domain.Model;
using EMS.Shared.Persistance;
using EMS.Shared.Promotion;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Modules.PromoMgmt.Domain.DependencyInjection
{
    public static class PromoDomainDependency
    {
        public static void AddPromoModule(this IServiceCollection services)
        {
            // Register your dependencies here
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddScoped<IDiscountStrategy, BulkBookingDiscountStrategy>();
            services.AddScoped<IDiscountStrategy, LoyaltyDiscountStrategy>();


            // Assuming IMapper is from AutoMapper and is set up elsewhere
            // services.AddSingleton<IMapper>(/* Your AutoMapper configuration here */);
        }
    }
}
