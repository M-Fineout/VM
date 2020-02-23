using Moq;
using Moq.Protected;
using Movies.Client;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Movies.Tests
{
    public class TestableClassWithApiAccessUnitTests
    {
        //Important to notice is that we did not have to start our client application, nor did we have to start our API. The API does not need to be up and running, as we won't actually communicate with it. So, this works, and this voids communication with the API.
        [Fact]
        public void GetMovie_On401Response_MustThrowUnauthorizedApiAccessException()
        {
            var httpClient = new HttpClient(new Return401UnauthorizedResponseHandler());
            var testableClass = new TestableClassWithApiAccess(httpClient);
            var cancellationTokenSource = new CancellationTokenSource();

            //because method is async, use ThrowsAsync
            Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => testableClass.GetMovie(cancellationTokenSource.Token));
        }

        [Fact]
        public void GetMovie_On401Response_MustThrowUnauthorizedApiAccessException_WithMoq()
        {

            //Build mock handler
            var unauthorizedResponseHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            unauthorizedResponseHttpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>( //return type of the method being mocked
                "SendAsync", //method name
                             //input parameters
                ItExpr.IsAny<HttpRequestMessage>(), //Any object of type HttpRequestMessage is accepted
                ItExpr.IsAny<CancellationToken>() //Any object of type CancellationToken is accepted
                ).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized //object to be returned
                });

            //Pass client mock handler
            var httpClient = new HttpClient(unauthorizedResponseHttpMessageHandlerMock.Object);

            var testableClass = new TestableClassWithApiAccess(httpClient);
            var cancellationTokenSource = new CancellationTokenSource();

            Assert.ThrowsAsync<UnauthorizedApiAccessException>(
                () => testableClass.GetMovie(cancellationTokenSource.Token));
        }
    }
}
