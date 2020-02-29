using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Messages
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMessageSender, SmsMessageSender>();//EmailMessageSender
            services.AddTransient<MessageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MessageService messageService)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async context =>
            {
                await context.Response.WriteAsync(messageService.Send(context));
            });
        }
    }
}
