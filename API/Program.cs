using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using LoggerClassLib;

namespace API
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddEnvironmentVariables()
          .Build();


        public static int Main(string[] args)
        {

            try
            {
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Host terminated unexpectedly");
                Console.Write(ex.ToString());
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((provider, context, loggerConfig) =>
                {
                    loggerConfig.WithSerilogConfiguration(provider, "BaseMicroserviceAPI", Configuration);
                });
        }

    }
}
