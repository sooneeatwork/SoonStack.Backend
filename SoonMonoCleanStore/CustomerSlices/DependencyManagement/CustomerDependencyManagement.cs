using CustomerSlices.Repository.CustomerTableMapper;
using CustomerSlices.Repository.RepoInterfaces;
using CustomerSlices.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSlices.DependencyManagement
{
    public static class CustomerDependencyManagement
    {
        public static void AddCustomerDependency(this IServiceCollection services) 
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //services.AddScoped<IProductRepository,ProductRepository>
            services.AddScoped<ICustomerTableMappers, CustomerTableMappers>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
