using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SummerUniPreAssignment.Middlewares
{
    public static class LogStuffExtension
    {
        //public static IApplicationBuilder UseLogStuff(this IApplicationBuilder app)
        //{
        //    return app.Use(next => new LogStuff(next).Invoke);
        //}

        //public static IApplicationBuilder UseGoodEtag(this IApplicationBuilder app)
        //{
        //    return app.Use(next => new GoodEtag(next).Invoke);
        //}
    }

    //public class LogStuff
    //{
    //    private RequestDelegate next;
    //    public LogStuff(RequestDelegate next)
    //    {
    //        this.next = next;
    //    }
    //    public async Task Invoke(HttpContext ctx)
    //    {
    //        Console.WriteLine("BEFORE ANYTHING");
    //        await next(ctx);
    //        Console.WriteLine("AFTER EVERYTHING");
    //    }
    //}

    //public class GoodEtag
    //{
    //    private RequestDelegate _next;
    //    public GoodEtag(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    public async Task Invoke(HttpContext ctx)
    //    {
    //        var response = ctx.Response;
    //        var originalStream = response.Body;

    //        using (var ms = new MemoryStream())
    //        {
    //            response.Body = ms;

    //            await _next(ctx);

    //            if (IsEtagSupported(response))
    //            {
    //                string checksum = CalculateChecksum(ms);

    //                response.Headers[HeaderNames.ETag] = checksum;

    //                if(ctx.Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var etag) && checksum == etag)
    //                {
    //                    response.StatusCode = StatusCodes.Status304NotModified;
    //                    return;
    //                }
    //            }

    //            ms.Position = 0;
    //            await ms.CopyToAsync(originalStream);
    //        }
    //    }

    //    private bool IsEtagSupported(HttpResponse response)
    //    {
    //        if(response.StatusCode != StatusCodes.Status200OK)
    //        {
    //            return false;
    //        }

    //        return true;
    //    }

    //    private static string CalculateChecksum(MemoryStream ms)
    //    {
    //        string checksum = string.Empty;

    //        using(var algo = SHA1.Create())
    //        {
    //            ms.Position = 0;
    //            byte[] bytes = algo.ComputeHash(ms);
    //            checksum = WebEncoders.Base64UrlEncode(bytes);
    //        }

    //        return checksum;
    //    }
    //}
}
