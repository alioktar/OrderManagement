using Microsoft.AspNetCore.Builder;
using OrderManagement.Core.Middlewares;

namespace OrderManagement.Core.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionCatcherMiddleware>();
        }
    }
}
