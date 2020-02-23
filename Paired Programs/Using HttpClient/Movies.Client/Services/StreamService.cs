using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Marvin.StreamExtensions;

namespace Movies.Client.Services
{
    public class StreamService : IIntegrationService
    {
        //private static HttpClient _httpClient = new HttpClient();

        //Note: Handler will only try to decompress when the content is actually compressed. It knows this by looking at the Content-Encoding header of the response.
        private static HttpClient _httpClient = new HttpClient(
            new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            });


        public StreamService()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:57863");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Clear();
        }
        public async Task Run()
        {
            //await GetPosterWithStream();
            //await GetPosterWithStreamAndCompletionMode();
            //await PostAndReadPosterWithStreams();
            await GetPosterWithGZipCompression();

            //For tests, start Api without debugging (under solution startup projects config)

            //await TestGetPosterWithoutStream();
            //await TestGetPosterWithStream();
            //await TestGetPosterWithStreamAndCompletionMode();
            //await TestPostPosterWithoutStream();
            //await TestPostPosterWithStream();
            //await TestPostAndReadPosterWithStreams();
        }
        
        private async Task GetPosterWithStream()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                var poster = stream.ReadAndDeserializeFromJson<Poster>();

                //Before extension method creation
                //using (var streamReader = new StreamReader(stream))
                //{
                //    using (var jsonTextReader = new JsonTextReader(streamReader))
                //    {
                //        var jsonSerializer = new JsonSerializer();
                //        var poster = jsonSerializer.Deserialize<Poster>(jsonTextReader);

                //        //Do something with the poster
                //    }
                //}

            }

        }

        private async Task GetPosterWithStreamAndCompletionMode()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //considering the operation complete when the response headers have arrived and not the full content. By doing so, we're working on the underlying stream of content directly, which means less memory has to be used, as the content doesn't have to be fully kept in memory. Moreover, it can also improve performance as we can start working with the data faster.
            using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                var poster = stream.ReadAndDeserializeFromJson<Poster>();

                //Before Extension method creation
                //using (var streamReader = new StreamReader(stream))
                //{
                //    using (var jsonTextReader = new JsonTextReader(streamReader))
                //    {
                //        var jsonSerializer = new JsonSerializer();
                //        var poster = jsonSerializer.Deserialize<Poster>(jsonTextReader);

                //        //Do something with the poster
                //    }
                //}
            }
        }
        private async Task GetPosterWithoutStream()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var posters = JsonConvert.DeserializeObject<Poster>(content);
        }

        private async Task GetPosterWithGZipCompression()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Only accept responses that are compressed into gzip format.
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                var poster = stream.ReadAndDeserializeFromJson<Poster>();

            }

        }

        private async Task PostPosterWithStream()
        {
            // generate a movie poster of 500KB
            var random = new Random();
            var generatedBytes = new byte[524288];
            random.NextBytes(generatedBytes);

            var posterForCreation = new PosterForCreation()
            {
                Name = "A new poster for The Big Lebowski",
                Bytes = generatedBytes
            };

            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(posterForCreation,
                new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(
              HttpMethod.Post,
              $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters"))
            {
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType =
                      new MediaTypeHeaderValue("application/json");

                    var response = await _httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var createdContent = await response.Content.ReadAsStringAsync();
                    var createdPoster = JsonConvert.DeserializeObject<Poster>(createdContent);
                }
            }
        }

        private async Task PostAndReadPosterWithStreams()
        {
            //generate a movie poster of 500kb
            var random = new Random();
            var generatedBytes = new byte[524288];
            random.NextBytes(generatedBytes);

            var posterForCreation = new PosterForCreation()
            {
                Name = "A new poster for The Big Lebowski",
                Bytes = generatedBytes
            };

            var memoryContentStream = new MemoryStream();

            memoryContentStream.SerializeToJsonAndWrite(posterForCreation, new UTF8Encoding(), 1024, true);

            //before extension method creation
            //using (var streamWriter = new StreamWriter(memoryContentStream, new UTF8Encoding(), 1024, true)) //leave stream open for 'POST'
            //{
            //    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            //    {
            //        var jsonSerializer = new JsonSerializer();
            //        jsonSerializer.Serialize(jsonTextWriter, posterForCreation);
            //        jsonTextWriter.Flush(); //This ensures that the buffer is flushed to the underlying TextWriter. If we don't do that, we might end up with an empty or incomplete stream.
            //    }
            //}

            //set our memory stream back to position 0, as that's where we want to start streaming it from.
            memoryContentStream.Seek(0, SeekOrigin.Begin);

            //wrap in a using since working with streams
            using (var request = new HttpRequestMessage(HttpMethod.Post, 
                "api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    //configure post request content
                    request.Content = streamContent;

                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //get response
                    using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();
                        var stream = await response.Content.ReadAsStreamAsync();
                        //deserialize response content
                        var poster = stream.ReadAndDeserializeFromJson<Poster>();
                    }
                }
            }

        }

        private async Task PostPosterWithoutStream()
        {
            // generate a movie poster of 500KB
            var random = new Random();
            var generatedBytes = new byte[524288];
            random.NextBytes(generatedBytes);

            var posterForCreation = new PosterForCreation()
            {
                Name = "A new poster for The Big Lebowski",
                Bytes = generatedBytes
            };

            var serializedPosterForCreation = JsonConvert.SerializeObject(posterForCreation);

            var request = new HttpRequestMessage(HttpMethod.Post,
                "api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/posters");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serializedPosterForCreation);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var createdMovie = JsonConvert.DeserializeObject<Poster>(content);
        }

        // ---------------TEST METHODS----------------------------------------------------------------------------------

        public async Task TestGetPosterWithoutStream()
        {
            //When the first request to an API comes in, the API might not by fully set up and running yet. By executing the request once, we ensure that the first run doesn't interfere with our test results. (warmup request)

            //warmup request
            await GetPosterWithoutStream();

            //start stopwatch
            var stopWatch = Stopwatch.StartNew();

            //run requests
            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithoutStream();
            }

            //stop stopwatch and log results
            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds without stream: {stopWatch.ElapsedMilliseconds},\n" +
                $"Averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
            
        }

        public async Task TestGetPosterWithStream()
        {
            //warmup request
            await GetPosterWithStream();

            //start stopwatch
            var stopWatch = Stopwatch.StartNew();

            //run requests
            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithStream();
            }

            //stop stopwatch and log results
            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds with stream: {stopWatch.ElapsedMilliseconds},\n" +
                $"Averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }

        public async Task TestGetPosterWithStreamAndCompletionMode()
        {
            //warmup request
            await GetPosterWithStreamAndCompletionMode();

            //start stopwatch
            var stopWatch = Stopwatch.StartNew();

            //run requests
            for (int i = 0; i < 200; i++)
            {
                await GetPosterWithStreamAndCompletionMode();
            }

            //stop stopwatch and log results
            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds with stream and completion mode: {stopWatch.ElapsedMilliseconds},\n" +
                $"Averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }

        private async Task TestPostPosterWithoutStream()
        {
            // warmup
            await PostPosterWithoutStream();

            // start stopwatch 
            var stopWatch = Stopwatch.StartNew();

            // run requests
            for (int i = 0; i < 200; i++)
            {
                await PostPosterWithoutStream();
            }

            // stop stopwatch
            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds without stream: " +
                $"{stopWatch.ElapsedMilliseconds}, " +
                $"averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }


        private async Task TestPostPosterWithStream()
        {
            // warmup
            await PostPosterWithStream();

            // start stopwatch 
            var stopWatch = Stopwatch.StartNew();

            // run requests
            for (int i = 0; i < 200; i++)
            {
                await PostPosterWithStream();
            }

            // stop stopwatch
            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds with stream: " +
                $"{stopWatch.ElapsedMilliseconds}, " +
                $"averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }


        private async Task TestPostAndReadPosterWithStreams()
        {
            // warmup
            await PostAndReadPosterWithStreams();

            // start stopwatch 
            var stopWatch = Stopwatch.StartNew();

            // run requests
            for (int i = 0; i < 200; i++)
            {
                await PostAndReadPosterWithStreams();
            }

            // stop stopwatch
            stopWatch.Stop();
            Console.WriteLine($"Elapsed milliseconds with stream on post and read: " +
                $"{stopWatch.ElapsedMilliseconds}, " +
                $"averaging {stopWatch.ElapsedMilliseconds / 200} milliseconds/request");
        }


    }
}
