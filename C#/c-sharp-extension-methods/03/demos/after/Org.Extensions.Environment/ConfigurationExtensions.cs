using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Org.Extensions.Environment
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Returns whether any configuration settings have been loaded
        /// </summary>
        /// <param name="config">Configuration object</param>
        /// <returns>True if any settings loaded</returns>
        public static bool IsLoaded(this IConfiguration config)
        {
            return config != null && config.AsEnumerable().Any();
        }

        /// <summary>
        /// Adds the standard config providers - JSON file, environment variables, config override & secrets override
        /// </summary>
        /// <param name="configBuilder">Configuration builder</param>
        /// <returns>Configuration builder</returns>
        public static IConfigurationBuilder AddStandardProviders(this IConfigurationBuilder configBuilder)
        {
            return configBuilder.AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .AddJsonFile("config/config.json", optional: true)
                                .AddJsonFile("secrets/secrets.json", optional: true);
        }
    }
}

