using Microsoft.Extensions.DependencyInjection;
using ProductSlices.ModuleServices;
using ProductSlices.Repository.ProductTableMapper;
using ProductSlices.Repository.Repository;
using SharedKernel.UseCases.ProductSlices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductSlices.DepdencyInjection
{
    public static class ProductDependencyManagement
    {
        public static void AddProductDependency(this IServiceCollection services) 
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //services.AddScoped<IProductRepository,ProductRepository>
            services.AddScoped<IProductTableMappers, ProductTableMappers>();
            services.AddScoped<IProductQueryServices, ProductQueryService>();
            services.AddScoped<IProductCommandServices, ProductCommandService>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
