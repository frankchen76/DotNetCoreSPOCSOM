using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SharePoint.Client;
using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;

namespace DotNetCoreConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigServices(args);
            var spoService = services.GetService<ISPOService>();
            await spoService.GetWeb();
        }
        private static ServiceProvider ConfigServices(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .AddUserSecrets<Program>()
                .Build();
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<SPOAppOptions>(configuration.GetSection(key: nameof(SPOAppOptions)));
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<ISPOService, SPOService>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
