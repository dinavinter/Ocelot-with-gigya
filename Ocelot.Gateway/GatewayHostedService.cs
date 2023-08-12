using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace OcelotBasic
{
    public class GatewayHostedService : IHostedService
    {
        private readonly IConfiguration _configuration;

        public GatewayHostedService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        private IWebHost _host;
         
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _host = new WebHostBuilder()
                .UseKestrel( )
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration() 
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
                .UseStartup<OcelotStartup>()
                .UseUrls($"http://localhost:{_configuration.GetValue<int>("Api:Host:Port")}")  

                .Build();
            
            return 
                _host.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _host
                .StopAsync(cancellationToken);
        }
    }
}