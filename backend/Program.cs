using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Your2020
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
