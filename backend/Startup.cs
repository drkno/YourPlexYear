using System;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Your2020.Service.Config;
using Your2020.Service.TautulliClient;

namespace Your2020
{
    public class Startup
    {
        private const string PoweredByHeaderName = "X-Powered-By";
        private const string PoweredByHeaderValue = "One small piece of fairy cake";
        private IConfigurationService ConfigurationService { get; }
        
        public Startup(IConfiguration configuration)
        {
            ConfigurationService = new ConfigurationService(configuration);
            Console.WriteLine(ConfigurationService.GetConfig().ToString());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(ConfigurationService.GetConfigurationDirectory()))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(500))
                .SetApplicationName("PlexSSO")
                .DisableAutomaticKeyGeneration();
            
            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ui";
            });

            services.AddHttpClient();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/idk";
                    options.Cookie.Name = "kPlexSSOKookieV2";
                    options.LoginPath = "/idk";
                    options.LogoutPath = "/idk";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    var cookieDomain = ConfigurationService.GetConfig().CookieDomain;
                    if (!string.IsNullOrWhiteSpace(cookieDomain))
                    {
                        options.Cookie.Domain = cookieDomain;
                    }
                });
            services.AddHealthChecks();
            services.AddSingleton<IConfigurationService>(ConfigurationService);
            services.AddSingleton<ITautulliClient, TautulliClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/idk");
            }
            app.Use((context, next) => {
                context.Response.Headers.Add(PoweredByHeaderName, PoweredByHeaderValue);
                return next.Invoke();
            });
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ui";
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/api/v1/healthcheck");
            });
        }
    }
}
