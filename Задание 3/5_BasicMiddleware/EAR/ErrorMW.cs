using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAR
{
    public class ErrorMW
    {
        RequestDelegate _next;
        public ErrorMW(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            switch (context.Response.StatusCode)
            {
                case 403:
                    await context.Response.WriteAsync("Acces Denied");
                    break;
                case 404:
                    await context.Response.WriteAsync("Page not found");
                    break;
            }
        }
    }
    public static class ErrorExtensions
    {
        public static IApplicationBuilder UseMyError(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMW>();
        }
    }
}
