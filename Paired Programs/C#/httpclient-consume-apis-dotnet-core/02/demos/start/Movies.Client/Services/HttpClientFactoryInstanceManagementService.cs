using Marvin.StreamExtensions;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class HttpClientFactoryInstanceManagementService : IIntegrationService
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly MoviesClient _moviesClient;

        public HttpClientFactoryInstanceManagementService(IHttpClientFactory httpClientFactory, MoviesClient moviesClient)
        {
            _httpClientFactory = httpClientFactory;
            _moviesClient = moviesClient;
        }
        public async Task Run()
        {
            //await TestDisposeHttpClient(_cancellationTokenSource.Token);
            //await TestReuseHttpClient(_cancellationTokenSource.Token);
            //await GetMoviesWithHttpClientFromFactory(_cancellationTokenSource.Token);
            //await GetMoviesWithNamedHttpClientFromFactory(_cancellationTokenSource.Token);
            //await GetMoviesWithTypedHttpClientFromFactory(_cancellationTokenSource.Token);
            await GetMoviesViaMoviesClient(_cancellationTokenSource.Token);
        }

        private async Task TestDisposeHttpClient(CancellationToken cancellationToken)
        {
            for (var i = 0; i < 10; i++)
            {
                //Leaves sockets open! Not good..
                //they are in a TIME_WAIT state. That means that nothing will be sent over those connections, but they can still receive packets from the server over them. These are the connections that we opened and closed by disposing of HttpClient. When HttpClient is disposed of, the open connection remains in this state for up to 240 seconds, and this can easily lead to socket exhaustion, which we want to avoid. 
                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage(
                        HttpMethod.Get, "https://google.com");

                    using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, 
                        cancellationToken))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine($"Request completed with status code: {response.StatusCode}");
                    }
                }
            }
        }

        private async Task TestReuseHttpClient(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            //one established connection to google.com, as expected. It's still in ESTABLISHED mode because our application is still up and running, so we haven't disposed of HttpClient and the underlying handler yet.
            for (var i = 0; i < 10; i++)
            {
                    var request = new HttpRequestMessage(
                        HttpMethod.Get, "https://google.com");

                    using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                        cancellationToken))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine($"Request completed with status code: {response.StatusCode}");
                    }
                
            }
        }

        private async Task GetMoviesWithHttpClientFromFactory(
            CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(
                       HttpMethod.Get, "http://localhost:57863/api/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();
        
            }

        }

        private async Task GetMoviesWithNamedHttpClientFromFactory(
           CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesClient");

            var request = new HttpRequestMessage(
                       HttpMethod.Get, "api/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();

            }

        }

        //private async Task GetMoviesWithTypedHttpClientFromFactory(
        //  CancellationToken cancellationToken)
        //{

        //    var request = new HttpRequestMessage(
        //               HttpMethod.Get, "api/movies");
        //    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

        //    using (var response = await _moviesClient.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
        //        cancellationToken))
        //    {
        //        var stream = await response.Content.ReadAsStreamAsync();
        //        response.EnsureSuccessStatusCode();
        //        var movies = stream.ReadAndDeserializeFromJson<List<Movie>>();

        //    }

        //}

        private async Task GetMoviesViaMoviesClient(CancellationToken cancellationToken)
        {
            var movies = await _moviesClient.GetMovies(cancellationToken); 
        }
    }
}
