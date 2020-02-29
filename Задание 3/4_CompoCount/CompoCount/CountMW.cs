using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompoCount
{
    public class CountMW
    {
        RequestDelegate _next;
        static double x = 101.32;
        static double y = 69.42;
        public CountMW(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"Result = {Math.Cbrt(Math.Log(x, y))}");
        }
    }
    public static class CountExtensions
    {
        public static IApplicationBuilder UseCount(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CountMW>();
        }
    }
}
