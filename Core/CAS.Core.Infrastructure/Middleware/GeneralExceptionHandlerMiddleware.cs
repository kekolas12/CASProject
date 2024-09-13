using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CAS.Core.Infrastructure.Middleware
{
    public class GeneralExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GeneralExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<GeneralExceptionHandlerMiddleware> logger)
        {
            await _next.Invoke(context);
        }




        public class ErrorHttpResponse
        {
            public ErrorHttpResponse(string friendlyMessage, string exceptionType = null)
            {
                ExceptionMessage = friendlyMessage ?? throw new ArgumentNullException(nameof(friendlyMessage));
                ExceptionType = exceptionType;
            }

            public string ExceptionMessage { get; }
            public string ExceptionType { get; }
        }
    }
}