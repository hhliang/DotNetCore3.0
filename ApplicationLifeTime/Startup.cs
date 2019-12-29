using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationLifeTime
{
    public class Startup
    {
       public Startup()
        {
            Program.Output("[Startup] Constructor - Called");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Program.Output("[Startup] ConfigureServices - Called");
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStarted.Register(() =>
            {
                Program.Output("[Startup] ApplicationLifetime - Started");
            });

            appLifetime.ApplicationStopping.Register(() =>
            {
                Program.Output("[Startup] ApplicationLifetime - Stopping");
            });

            appLifetime.ApplicationStopped.Register(() =>
            {
                Thread.Sleep(5 * 1000);
                Program.Output("[Startup] ApplicationLifetime - Stopped");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });

            // For trigger stop WebHost
            var thread = new Thread(() =>
            {
                Thread.Sleep(5 * 1000);
                Program.Output("[Startup] Trigger stop WebHost");
                appLifetime.StopApplication();
            });
            thread.Start();

            Program.Output("[Startup] Configure - Called");
        }
    }
}
