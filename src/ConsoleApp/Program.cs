using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using ConsoleApp.Extensions;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isDebugging = !(Debugger.IsAttached || args.Contains("--console"));
            var hostBuilder = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<IntegrationService>();
                    // services.AddLogging(builder =>
                    // {
                    //     builder.SetMinimumLevel(LogLevel.Information);
                    //     builder.AddNLog("nlog.config");
                    // });
                });
            
            if (isDebugging)
            {
                await hostBuilder.RunTheServiceAsync();
            }
            else
            {
                await hostBuilder.RunConsoleAsync();
            }
        }        
    }
}
