using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using YourPlexYear.Service;

namespace YourPlexYear
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Contains("--healthcheck"))
            {
                await HealthChecker.CheckHealth($"http://127.0.0.1:4201/api/v1/healthcheck");
                return;
            }

            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddCommandLine(args, new Dictionary<string, string>()
                    {
                        {"-s", "server"},
                        {"--server", "server"},
                        {"-p", "preferences"},
                        {"--preferences", "preferences"},
                        {"-c", "cookie_domain"},
                        {"--cookie-domain", "cookie_domain"},
                        {"--config", "config"}
                    });
                })
                .ConfigureKestrel((context, options) => options.AddServerHeader = false)
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:4201/")
                .Build()
                .Run();
        }
    }
}
