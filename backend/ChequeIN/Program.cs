using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChequeIN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder()
              .UseStartup<Startup>()
              .Build()
              .Run();
        }

        // Only used by EF Tooling
        // Thank you: https://wildermuth.com/2017/07/06/Program-cs-in-ASP-NET-Core-2-0
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
              .ConfigureAppConfiguration((ctx, cfg) =>
              {
                  cfg.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true)
                  .AddEnvironmentVariables();
              })
              .ConfigureLogging((ctx, logging) => { })
              .UseStartup<Startup>()
              .UseSetting("DesignTime", "true")
              .Build();
        }
    }
}
