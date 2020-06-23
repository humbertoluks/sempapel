using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ConsoleApp.Extensions;
using Repository.Data;
using Repository.Interfaces;
using Repository;

namespace ConsoleApp
{
    class Program
    {

        public static IConfiguration Configuration { get; }
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
                    services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("database")));
                    services.AddTransient<IGuiaRepository, GuiaRepository>();
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

