using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

[assembly: HostingStartup(typeof(OcelotBasic.ServiceKeyInjection))]

namespace OcelotBasic
{
    public class ServiceKeyInjection : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder
           
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
                            true)
                        .AddJsonFile("ocelot.json")
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    s.AddCors(x => x.AddDefaultPolicy(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
                    s.AddGigyaOidc();
                    s.AddOcelot();
                })
                .Configure(app =>
                {

                    app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
                        .UseAuthentication()
                        .UseOcelot(configuration =>
                        {
                        })
                        .Wait();
                });
        }
    }
}