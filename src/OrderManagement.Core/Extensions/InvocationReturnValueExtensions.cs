using Castle.DynamicProxy;

namespace OrderManagement.Core.Extensions
{
    public static class InvocationReturnValueExtensions
    {
        public static void ContinueWith<T>(IInvocation invocation, Action<T?> continuationAction) where T : class, new()
        {
            ((Task)invocation.ReturnValue).ContinueWith((task) =>
            {
                if (task.Status != TaskStatus.Faulted)
                {
                    var returnType = task.GetType()
                          .GetProperty("Result")?
                          .GetValue(task, null) as T;

                    continuationAction(returnType);
                }
            });
        }
    }
}
