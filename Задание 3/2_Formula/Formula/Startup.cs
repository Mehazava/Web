using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Formula
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            double x = 32.32;
            double y = 25.11;
            double z = 13.15;

            app.UseRouting();
            //app.UseCalculate();
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"Result is {Math.Pow(y, Math.Cos(Math.Sqrt(x) + z))}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Fail!");
                });
            });
        }
    }
}
