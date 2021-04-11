using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreConsoleApp
{
    public interface ISPOService
    {
        Task GetWeb();
    }

    public class SPOService : ISPOService
    {
        private IAuthenticationService _authService = null;
        public SPOService(IAuthenticationService authService)
        {
            _authService = authService;
        }
        public async Task GetWeb()
        {
            Uri site = new Uri("https://m365x725618.sharepoint.com/sites/FrankCommunication1");

            using (var context = _authService.GetContext(site))
            {
                context.Load(context.Web, p => p.Title);
                await context.ExecuteQueryAsync();
                Console.WriteLine($"Title: {context.Web.Title}");
            }
        }
    }
}
