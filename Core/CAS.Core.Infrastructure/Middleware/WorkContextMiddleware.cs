using Microsoft.AspNetCore.Http;

namespace CAS.Core.Infrastructure.Middleware
{
    public class WorkContextMiddleware
    {
        private readonly RequestDelegate _next;

        public WorkContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context?.Request == null)
            {
                return;
            }

            await _next(context);
        }
    }
}
