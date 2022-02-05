using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Repository.Base;
using OrderManagement.Repository.EntityFreamework;
using OrderManagement.Repository.EntityFreamework.Contexts;
using OrderManagement.Repository.EntityFreamework.Repositories;

namespace OrderManagement.Service.Extensions
{
    public static class DataLayerConfigurationExtension
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderManagementDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default"),
                    builder => builder.MigrationsAssembly(typeof(OrderManagementDbContext).Assembly.GetName().Name)), ServiceLifetime.Transient);

            services.AddScoped<DbContext>(provider => provider.GetRequiredService<OrderManagementDbContext>());
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static void UseInitializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<OrderManagementDbContext>().Database.Migrate();
            }
        }
    }
}
