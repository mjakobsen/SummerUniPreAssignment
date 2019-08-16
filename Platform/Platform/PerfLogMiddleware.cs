using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Platform
{
    public static class PerfLogExtensions
    {
        public static IApplicationBuilder UsePerfLog(this IApplicationBuilder app)
        {
            return app.Use(next => new PerfLogMiddleware(next).Invoke);
        }
    }

    class PerfLogMiddleware
    {
        private RequestDelegate _next;
        public PerfLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext ctx)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next(ctx);
            Console.WriteLine("Action execution time: " + stopWatch.ElapsedMilliseconds);
        }
    }
}
