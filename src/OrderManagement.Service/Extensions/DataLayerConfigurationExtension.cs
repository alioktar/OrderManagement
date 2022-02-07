using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Repository.Base;
using OrderManagement.Repository.EntityFreamework;
using OrderManagement.Repository.EntityFreamework.Contexts;

namespace OrderManagement.Service.Extensions
{
    public static class DataLayerConfigurationExtension
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderManagementDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default"),
                    builder => builder.MigrationsAssembly(typeof(OrderManagementDbContext).Assembly.GetName().Name)
                )
            );

            services.AddScoped<DbContext>(provider => provider.GetRequiredService<OrderManagementDbContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static void UseDatabaseInitializer(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<OrderManagementDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
