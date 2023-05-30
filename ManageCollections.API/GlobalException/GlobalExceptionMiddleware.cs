using System.Net;
using Serilog;

namespace ManageCollections.API.GlobalException
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode < 300)
                {
                    Log.Warning($"Request:{httpContext.Request.Path} Status_Code:{httpContext.Response.StatusCode}");
                }
                else
                {
                    Log.Error($" Request:{httpContext.Request.Path} Status_Code:{httpContext.Response.StatusCode}");
                }
            }
            catch (NotImplementedException ex)
            {
                await HandleExceptionAsync
                  (httpContext, ex.Message, HttpStatusCode.NotImplemented, "Not found for your request!");
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync
                      (httpContext, ex.Message, HttpStatusCode.NotFound, "Not found for your request!");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync
                      (httpContext, ex.Message, HttpStatusCode.InternalServerError, "Oops something went wrong!");
            }
        }

        public async Task HandleExceptionAsync
           (HttpContext httpContext, string exMessage, HttpStatusCode httpStatusCode, string message)
        {
            Log.Error("EXCEPTION: " + $"\nDatetime:{DateTime.Now} | Message:{exMessage} | Path:{httpContext.Request.Path}");
            HttpResponse response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;


            ErrorDto error = new ErrorDto
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

            await response.WriteAsync(error.ToString());

        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
