using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Movies.Client.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Client
{
    class Program
    {
 
        static async Task Main(string[] args)
        {
            // create a new ServiceCollection 
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            // create a new ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            // For demo purposes: overall catch-all to log any exception that might 
            // happen to the console & wait for key input afterwards so we can easily 
            // inspect the issue.  
            try
            {
                // Run our IntegrationService containing all samples and
                // await this call to ensure the application doesn't 
                // prematurely exit.
                await serviceProvider.GetService<IIntegrationService>().Run();
            }
            catch (Exception generalException)
            {
                // log the exception
                var logger = serviceProvider.GetService<ILogger<Program>>();
                logger.LogError(generalException, 
                    "An exception happened while running the integration service.");
            }
            
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add loggers           
            serviceCollection.AddSingleton(new LoggerFactory()
                  .AddConsole()
                  .AddDebug());

            serviceCollection.AddLogging();

            //use HttpClientFactory to set up clients from config, rather than using static clients
            serviceCollection.AddHttpClient("MoviesClient", client =>
            {
                client.BaseAddress = new Uri("http://localhost:57863");
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            })
            .AddHttpMessageHandler(handler => new TimeoutDelegatingHandler(TimeSpan.FromSeconds(20))) //must be lower than above default Timeout, or else we will get a TaskCanceledException, not a TimeoutException
            .AddHttpMessageHandler(handler => new RetryPolicyDelegatingHandler(2))
            //Primary message handler is always last in the pipeline
            .ConfigurePrimaryHttpMessageHandler(handler =>
            new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            });

            //adding typed instances of httpclient
            //serviceCollection.AddHttpClient<MoviesClient>(client =>
            //{
            //    client.BaseAddress = new Uri("http://localhost:57863");
            //    client.Timeout = new TimeSpan(0, 0, 30);
            //    client.DefaultRequestHeaders.Clear();
            //})
            //.ConfigurePrimaryHttpMessageHandler(handler =>
            //new HttpClientHandler()
            //{
            //    AutomaticDecompression = System.Net.DecompressionMethods.GZip
            //});

            //With base configuration done in the MoviesClient ctor itself
            serviceCollection.AddHttpClient<MoviesClient>()
                .ConfigurePrimaryHttpMessageHandler(handler =>
                new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });

            // register the integration service on our container with a 
            // scoped lifetime

            // For the CRUD demos
            //serviceCollection.AddScoped<IIntegrationService, CRUDService>();

            // For the partial update demos
            // serviceCollection.AddScoped<IIntegrationService, PartialUpdateService>();

            // For the stream demos
            // serviceCollection.AddScoped<IIntegrationService, StreamService>();

            // For the cancellation demos
            // serviceCollection.AddScoped<IIntegrationService, CancellationService>();

            // For the HttpClientFactory demos
            //serviceCollection.AddScoped<IIntegrationService, HttpClientFactoryInstanceManagementService>();

            // For the dealing with errors and faults demos
            // serviceCollection.AddScoped<IIntegrationService, DealingWithErrorsAndFaultsService>();

            // For the custom http handlers demos
            serviceCollection.AddScoped<IIntegrationService, HttpHandlersService>();     
        }
    }
}
