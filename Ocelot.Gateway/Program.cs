using System;
using System.IO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotBasic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // config
                    //     .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    //     .AddJsonFile("appsettings.json", true, true)
                    //     .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    //     .AddOcelot(hostingContext.HostingEnvironment)
                    //     .AddEnvironmentVariables();
                })
                .ConfigureServices(s =>
                {
                    // s.AddCors(x=>x.AddDefaultPolicy(o=>o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
                    // s.AddGigyaOidc();
                    // s.AddOcelot();
                    
                    // s.AddAuthentication()
                    //     .AddOpenIdConnect(authenticationProviderKey, configureOptions: options =>
                    //     {
                    //         options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //         options.Authority = "https://fidm.us1-st1.gigya.com/oidc/op/v1.0/3__NKd98KtcRCL_Z98TO7bbTtMhZqe83A4hOjMA2wblxL8PAoduwgW9FTvdQ6OqYIB"; // IDENTITY SERVER URL
                    //          options.ClientId = "AHwiThd52-bPTYYXq0UFOosC";
                    //         options.ClientSecret = "poVgJrnr3fn7bxq-fVkabe-EDXPYmREK3AX1alWO3UwgLb4BeJ-dALONF5xJhFRXZRZn9QT79V-vK6KICyGcxg";
                    //
                    //     });

                    // s.AddAdministration("/administration", "secret");
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .UseIISIntegration()
                .Configure(app =>
                { 
                    // app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
                    //     .UseAuthentication() 
                    //     .UseOcelot()
                    //     .Wait();
                })
                .Build()
                .Run();
        }

     
    }
}