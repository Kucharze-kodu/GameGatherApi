using System.Diagnostics;

namespace GameGather.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                var request = context.Request;
                var response = context.Response;

                var method = request.Method;
                var path = request.Path;
                var statusCode = response.StatusCode;
                var elapsedMs = stopwatch.ElapsedMilliseconds;
                var ipAddress = context.Connection.RemoteIpAddress?.ToString();
                var user = context.User.Identity?.IsAuthenticated == true
                    ? context.User.Identity.Name
                    : "Anonymous";

                _logger.LogInformation(
                    "HTTP {method} {url} responded {statusCode} in {elapsed} ms | IP: {ip} | User: {user}",
                    method,
                    path,
                    statusCode,
                    elapsedMs,
                    ipAddress,
                    user
                );
            }
        }
    }

}
