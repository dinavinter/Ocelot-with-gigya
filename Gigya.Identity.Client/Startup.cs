using Gigya.Identity.Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
namespace Gigya.Identity.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllersWithViews();
       
            services.AddRazorPages(x =>
                {
                    x.Conventions
                        .AddFolderApplicationModelConvention("/GigyaProxy",
                            model => model.Filters.Add(new ProxyPageFilter()));
                    
                    x.Conventions
                        .AddPageApplicationModelConvention("/proxy",
                            model => model.Filters.Add(new ProxyPageFilter()));
                }
                

            );
          
            services.AddScoped(sp =>
                {
                    sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Items
                        .TryGetValue(typeof(GigyaOP), out var op);

                    return op as GigyaOP ?? new GigyaOP()
                    {
                        DataCenter = "us1",
                        ApiKey = "3__NKd98KtcRCL_Z98TO7bbTtMhZqe83A4hOjMA2wblxL8PAoduwgW9FTvdQ6OqYIB"
                    };
                }
            );

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // In production, the React files will be served from this directory
            // services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseRouting();
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                     pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // app.UseSpa(spa =>
            // {
            //     spa.Options.SourcePath = "ClientApp";
            //
            //     if (env.IsDevelopment())
            //     {
            //         spa.UseReactDevelopmentServer(npmScript: "start");
            //     }
            // });
        }
    }
}