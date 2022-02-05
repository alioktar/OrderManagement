using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
using OrderManagement.Core.Extensions;
using OrderManagement.Core.IoC;
using OrderManagement.Core.Utilities.Interceptors;

namespace OrderManagement.Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetRequiredService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method?.ReflectedType?.FullName}.{invocation.Method?.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                /// TODO : Return value will be convert to task
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();

            /// TODO : Wil be test.
            InvocationReturnValueExtensions.ContinueWith<object>(invocation, (value) =>
            {
                _cacheManager.Add(key, value, _duration);
            });
        }
    }
}
