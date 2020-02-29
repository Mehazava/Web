using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formula
{
    public class CalculateMW
    {
        RequestDelegate _next;
        CalculateMW(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
    public static class CalculateExtensions
    {
        public static IApplicationBuilder UseCalculate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CalculateMW>();
        }
    }
}
