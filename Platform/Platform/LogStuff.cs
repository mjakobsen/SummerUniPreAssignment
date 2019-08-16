using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Platform
{
    public static class LogStuffExtensions
    {
        public static IApplicationBuilder UseLogStuff(this IApplicationBuilder app)
        {
            return app.Use(next => new LogStuff(next).Invoke);
        }
    }

    class LogStuff
    {
        private RequestDelegate next;
        public LogStuff(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext ctx)
        {
            var path = ctx.Request.Path;
            Console.WriteLine($"BEFORE ANYTHING ON PATH: {path}");
            await next(ctx);
            Console.WriteLine("AFTER EVERYTHING");
        }
    }
}
