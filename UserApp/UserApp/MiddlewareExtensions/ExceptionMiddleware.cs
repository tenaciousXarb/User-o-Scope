using AppUser.BusinessServices.DTO;
using Serilog;
using System.Net;

namespace AppUser.API.MiddlewareExtensions
{
    /// <summary>
    /// ExceptionMiddleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// ExceptionMiddleware Constructor
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="BadHttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (httpContext.Request.Headers["Authorization"].ToString() != null && httpContext.Request.Headers["Authorization"].ToString().StartsWith("Bearer"))
                {
                    await _next(httpContext);
                }
                else
                {
                    if (httpContext.Request.Path == "/api/authentications")
                    {
                        await _next(httpContext);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("You're not authorized to access this page");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Caught Exception: {ex.GetType().Name} || Message: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var message = exception.Message;

            if (exception is InvalidOperationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
