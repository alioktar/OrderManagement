using Castle.DynamicProxy;
using System.Reflection;

namespace OrderManagement.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<InterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)?
                .GetCustomAttributes<InterceptionBaseAttribute>(true);

            if (methodAttributes != null)
                classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
