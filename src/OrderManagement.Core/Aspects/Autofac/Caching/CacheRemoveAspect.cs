using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
using OrderManagement.Core.IoC;
using OrderManagement.Core.Utilities.Interceptors;

namespace OrderManagement.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string[] _keys;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(params string[] keys)
        {
            _keys = keys;
            _cacheManager = ServiceTool.ServiceProvider.GetRequiredService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            foreach (var key in _keys)
            {
                _cacheManager.RemoveByPattern(key);
            }
        }
    }
}
