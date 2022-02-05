using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderManagement.Core.IoC;
using StackExchange.Redis;
using System.Text;
using System.Text.RegularExpressions;

namespace OrderManagement.Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private string _redisConfig;
        private readonly IDistributedCache _cache;

        public RedisCacheManager(IDistributedCache cache)
        {
            _cache = cache;
            var configuration = ServiceTool.ServiceProvider.GetRequiredService<IConfiguration>();
            _redisConfig = configuration["Redis"] ?? throw new ArgumentNullException("Redis connection string cannot be null.");
        }

        public T? Get<T>(string key)
        {
            var valueString = _cache.GetString(key);
            if (!string.IsNullOrEmpty(valueString))
            {
                var valueObject = JsonConvert.DeserializeObject<T>(valueString, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                return valueObject;
            }

            return default(T);
        }

        public object? Get(string key)
        {
            var valueString = _cache.GetString(key);
            if (!string.IsNullOrEmpty(valueString))
            {
                return JsonConvert.DeserializeObject(valueString, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }

            return null;
        }

        public void Add<T>(string key, T valueObject, int expiration = 60)
        {
            var valueString = JsonConvert.SerializeObject(valueObject, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            _cache.SetString(key, valueString, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddMinutes(expiration) });
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            using (var redisConnection = ConnectionMultiplexer.Connect(_redisConfig))
            {
                var redisServer = redisConnection.GetServer(redisConnection.GetEndPoints().First());
                var redisDatabase = redisConnection.GetDatabase();

                var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var keysToRemove = redisServer.Keys().Where(d => regex.IsMatch(d.ToString())).Select(d => d.ToString()).ToList();

                foreach (var key in keysToRemove)
                {
                    redisDatabase.KeyDelete(key);
                }
            }
        }

        public bool IsAdd(string key)
        {
            return !string.IsNullOrEmpty(_cache.GetString(key)) ? true : false;
        }
    }
}
