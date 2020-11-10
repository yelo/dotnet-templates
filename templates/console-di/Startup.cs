using consoledi.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace consoledi
{
    internal static class Startup
    {
        private static async Task Main()
        {
            // Create the service collection.
            var services = new ServiceCollection();

            // Setup the service collection.
            ConfigureLogging(services);
            ConfigureOptions(services);
            ConfigureServices(services);

            // Create the service provider and run the application.
            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<App>().Run().ConfigureAwait(false);
        }

        /// <summary>
        /// Configure the logging of the application.
        /// </summary>
        /// <param name="services">Service collection to which we want to add logging capabilities.</param>
        private static void ConfigureLogging(IServiceCollection services)
        {
            services.AddLogging(options =>
            {
                options.ClearProviders();
                options.AddConsole();
                options.SetMinimumLevel(LogLevel.Debug);
            });
        }

        /// <summary>
        /// Configure the application settings/options.
        /// </summary>
        /// <param name="services">Service collection to which we want to setup the options for.</param>
        private static void ConfigureOptions(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection("Configuration"));
        }

        /// <summary>
        /// Configure the application services.
        /// </summary>
        /// <param name="services">Service collection to which we want to setup the services.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<App>();
        }
    }
}