using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace Your2020.Service.Config
{
    public class ConfigurationService : IConfigurationService
    {
        private const string ConfigurationDirectoryKey = "config";
        private const string ConfigurationFileName = "config.json";

        private const string CookieDomainConfigurationKey = "cookie_domain";

        private readonly string _configDirectory;
        private readonly YourYearConfig _config;

        public ConfigurationService(IConfiguration configuration)
        {
            _configDirectory = configuration[ConfigurationDirectoryKey] ?? Environment.CurrentDirectory;
            var configFile = Path.Join(_configDirectory, ConfigurationFileName);
            _config = LoadConfig(configFile, configuration);
        }

        private YourYearConfig LoadConfig(string configFile, IConfiguration configuration)
        {
            var serialiserConfig = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            YourYearConfig yourYearConfig;
            if (File.Exists(configFile))
            {
                yourYearConfig = JsonSerializer.Deserialize<YourYearConfig>(File.ReadAllText(configFile), serialiserConfig);
                UpdateConfigWithCliOptions(ref yourYearConfig, configuration);
            }
            else
            {
                yourYearConfig = new YourYearConfig();
                UpdateConfigWithCliOptions(ref yourYearConfig, configuration);
                File.WriteAllText(configFile, JsonSerializer.Serialize<YourYearConfig>(yourYearConfig, serialiserConfig));
            }

            return yourYearConfig;
        }

        private void UpdateConfigWithCliOptions(ref YourYearConfig yourYearConfig, IConfiguration configuration)
        {   
            string cookieDomain;
            if ((cookieDomain = configuration[CookieDomainConfigurationKey]) != null)
            {
                yourYearConfig.CookieDomain = cookieDomain;
            }
        }

        public YourYearConfig GetConfig()
        {
            return _config;
        }

        public string GetConfigurationDirectory()
        {
            return _configDirectory;
        }

        public string GetOmbiUrl()
        {
            return _config.OmbiPublicHostname;
        }

        public string GetTautulliUrl()
        {
            return _config.TautulliPublicHostname;
        }

        public string GetTautulliApiKey()
        {
            return _config.TautulliApiKey;
        }

        public string GetMoviesLibraryId()
        {
            throw new NotImplementedException();
        }

        public string GetTvShowsLibraryId()
        {
            throw new NotImplementedException();
        }
    }
}

