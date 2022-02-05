using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
using OrderManagement.Core.CrossCuttingConcerns.Caching.Redis;
using OrderManagement.Core.IoC;

namespace OrderManagement.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services) => ServiceTool.Create(services);

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis"];
            });
            services.AddSingleton<ICacheManager, RedisCacheManager>();
            return services;
        }
    }
}
