using Microsoft.AspNetCore.Mvc.Filters;

namespace ManageCollections.API.Filters
{
    public class CustomHeaderAttribute:ResultFilterAttribute
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public CustomHeaderAttribute(string headerName, string headerValue)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // Add or modify the desired header in the response
            context.HttpContext.Response.Headers.Add(_headerName, _headerValue);

            base.OnResultExecuting(context);
        }
    }
}
