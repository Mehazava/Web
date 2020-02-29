using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAR
{
    public class RoutingMW
    {
        RequestDelegate _next;
        public RoutingMW(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            switch (context.Request.Path.Value)
            {
                case "/1":
                    await context.Response.WriteAsync("Page for cool kids");
                    break;
                case "/2":
                    await context.Response.WriteAsync("Page for losers");
                    break;
                default:
                    context.Response.StatusCode = 404;
                    break;
            }
        }
    }
    public static class RoutingExtensions
    {
        public static IApplicationBuilder UseMyRouting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoutingMW>();
        }
    }
}
