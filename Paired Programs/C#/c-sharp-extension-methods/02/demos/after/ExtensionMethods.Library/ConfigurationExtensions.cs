using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ExtensionMethods.Library
{
    public static class ConfigurationExtensions //sponsor class --must be static
    {
        //As per below IsLoaded method:

        //the first argument of the method specifies the type you're going to extend using the 'this' keyword. --We're extending IConfiguration and when the method runs, it will be passed an instance of the object, which will be called 'config'.
        //This is the typical signature for an extension method, a public static method in a public static class using 'this' to identify the target type which the method extends.
        //When I build this, anyone can reference my library and use my IsLoaded method with any object that implements Microsoft's IConfiguration interface.
        

        /// <summary>
        /// Tells us if the config object has been loaded.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool IsLoaded(this IConfiguration config)
        { 
            return config != null && config.AsEnumerable().Any();
        }

        public static IConfigurationBuilder AddStandardProviders(this IConfigurationBuilder configBuilder)
        {
            return configBuilder.AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables()
                                .AddJsonFile("config/config.json", optional: true)
                                .AddJsonFile("secrets/secrets.json", optional: true);
        }
    }
}

