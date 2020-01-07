using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddleWare
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("First Middleware in. \r\n");
                await next.Invoke();
                await context.Response.WriteAsync("First Middleware out. \r\n");
            });

            //新增路由
            app.Map("/second", mapApp =>
            {
                mapApp.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Second Middleware in. \r\n");
                    await next.Invoke();
                    await context.Response.WriteAsync("Second Middleware out. \r\n");
                });
                mapApp.Run(async context =>
                {
                    await context.Response.WriteAsync("Second. \r\n");
                });
            });

            //全域註冊
            app.UseMiddleware<ThirdMiddleware>();

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello World! \r\n");
            });
        }
    }
}
