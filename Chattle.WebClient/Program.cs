using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazorise;
using Blazorise.Bulma;
using Blazorise.Icons.FontAwesome;

namespace Chattle.WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazorise();
            builder.Services.AddBulmaProviders();
            builder.Services.AddFontAwesomeIcons();

            var host = builder.Build();
            host.Services.UseBulmaProviders();
            host.Services.UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
