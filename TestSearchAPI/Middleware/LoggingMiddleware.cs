using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable
namespace TestSearchAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Skip logging if the request path matches Hangfire endpoint
            if (context.Request.Path.StartsWithSegments("/hangfire"))
            {
                await _next(context);
                return;
            }
            // Log information about the incoming request using Serilog
            Log.Information($"Request {context.Request.Method}: {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Log information about the outgoing response using Serilog
            Log.Information($"Response {context.Request.Method}: {context.Request.Path} => {context.Response.StatusCode}");
        }
    }
}
