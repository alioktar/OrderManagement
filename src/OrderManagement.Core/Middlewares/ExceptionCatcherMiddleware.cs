using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderManagement.Core.Utilities.Exceptions;
using OrderManagement.Core.Utilities.Response;
using System.Net;
using System.Security;

namespace OrderManagement.Core.Middlewares
{
    public class ExceptionCatcherMiddleware
    {
        private readonly ILogger<ExceptionCatcherMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ExceptionCatcherMiddleware(RequestDelegate next, ILogger<ExceptionCatcherMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ExceptionCatcherMiddleware => {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse(ex.Message);

            if (ex.GetType() == typeof(ValidationException) || ex.GetType() == typeof(ApplicationException))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (ex.GetType() == typeof(UnauthorizedAccessException) || ex.GetType() == typeof(SecurityException))
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (ex.GetType() == typeof(GeneralException) || ex.GetType().IsSubclassOf(typeof(GeneralException)))
            {
                httpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                errorResponse = new ErrorResponse("Something went wrong. Please try again.");
            }

            await httpContext.Response.WriteAsync(
                JsonConvert.SerializeObject(
                    errorResponse,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                )
            );
        }
    }
}
