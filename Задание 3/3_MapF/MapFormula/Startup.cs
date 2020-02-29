using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MapFormula
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

            app.Map("/1", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync($"Result: {Math.Log(Math.Min(x, y), z)}");
                });
            });
            app.Map("/2", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync($"Result: {Math.Cbrt(x)*Math.Log2(y)/Math.Exp(z)}");
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("rip");
            });
        }
    }
}
