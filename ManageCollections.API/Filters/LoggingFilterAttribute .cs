using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManageCollections.API.Filters
{
    public class LoggingFilterAttribute : TypeFilterAttribute
    {
        public LoggingFilterAttribute() : base(typeof(LoggingFilterImpl))
        {
        }

        private class LoggingFilterImpl : IResultFilter
        {
            private readonly ILogger<LoggingFilterImpl> _logger;

            public LoggingFilterImpl(ILogger<LoggingFilterImpl> logger)
            {
                _logger = logger;
            }

            public void OnResultExecuting(ResultExecutingContext context)
            {
                _logger.LogInformation("Request details: {RequestMethod} {RequestPath}",
                    context.HttpContext.Request.Method, context.HttpContext.Request.Path);
            }

            public void OnResultExecuted(ResultExecutedContext context)
            {
                _logger.LogInformation("Response details: {StatusCode}",
                    context.HttpContext.Response.StatusCode);
            }
        }
    }
}
