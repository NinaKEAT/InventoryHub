using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace ServerApp.Middleware
{
    /// <summary>
    /// Simple request logging middleware for diagnostics during development.
    /// Logs path and elapsed time for requests. Keep lightweight so it can be left enabled in tests.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();
                var path = context.Request?.Path.Value ?? "<unknown>";
                Console.WriteLine($"[Request] {path} completed in {sw.ElapsedMilliseconds}ms");
            }
        }
    }

    // Extension helper for registration
    public static class RequestLoggingExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}