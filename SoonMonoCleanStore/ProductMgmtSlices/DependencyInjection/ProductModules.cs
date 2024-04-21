using Microsoft.Extensions.DependencyInjection;
using ProductMgmtSlices.ModuleServices;
using ProductMgmtSlices.Repository.Repository;
using SharedKernel.UseCases.ProductSlices;
using System.Reflection;

namespace ProductMgmtSlices.DependencyInjection
{
    public static class ProductModules
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
