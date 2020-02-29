using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAR
{
    public class AuthMW
    {
        RequestDelegate _next;
        static string Name;
        public AuthMW(RequestDelegate next, string name)
        {
            _next = next;
            Name = name;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!(string.IsNullOrWhiteSpace(context.Request.Query["name"])) && context.Request.Query["name"] == Name)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 403;
            }
            
        }
    }
    public static class AuthExtensions
    {
        public static IApplicationBuilder UseMyAuth(this IApplicationBuilder builder, string name)
        {
            return builder.UseMiddleware<AuthMW>(name);
        }
    }
}
