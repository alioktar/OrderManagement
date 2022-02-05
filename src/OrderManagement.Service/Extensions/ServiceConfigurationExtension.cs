using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Service.Services.Base;
using OrderManagement.Service.Services.Concrete;

namespace OrderManagement.Service.Extensions
{
    public static class ServiceConfigurationExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayer(configuration).AddMapper();

            services.AddTransient<ICustomerService, CustomerService>();

            return services;
        }
    }
}
