using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Core.Extensions;
using OrderManagement.Service.Services.Base;
using OrderManagement.Service.Services.Concrete;

namespace OrderManagement.Service.Extensions
{
    public static class ServiceConfigurationExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayer(configuration)
                    .AddMapper();
                    //.AddRedisCache(configuration)
                    //.AddDependencyResolvers();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerOrderService, CustomerOrderService>();

            return services;
        }

        public static void UseServiceMiddlewares(this IApplicationBuilder app)
        {
            app.UseExceptionMiddleware();
            app.UseDatabaseInitializer();
        }
    }
}
