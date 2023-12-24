using Microsoft.Extensions.DependencyInjection;
using OrderSlices.Repository.Repository;
using OrderSlices.Repository.Repository.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderSlices.DependencyInjection
{
    public static class OrderDependencyManagement
    {
        public static void AddOrderDependency(this IServiceCollection services) 
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //services.AddScoped<IProductRepository,ProductRepository>
            services.AddScoped<IOrderItemTableMap, OrderItemTableMapper>();
            services.AddScoped<IOrderTableMap, OrderTableMap>();
            services.AddScoped<IOrderItemTableMap, OrderItemTableMapper>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
