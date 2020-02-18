using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client
{
    public class TimeoutDelegatingHandler : DelegatingHandler
    {
        //default timeout value of HttpClient
        private readonly TimeSpan _timeOut = TimeSpan.FromSeconds(100);

        public TimeoutDelegatingHandler(TimeSpan timeOut) : base()
        {
            _timeOut = timeOut;
        }

        public TimeoutDelegatingHandler(HttpMessageHandler innerHandler, TimeSpan timeOut) : base(innerHandler)
        {
            _timeOut = timeOut;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // To implement this, we're using a new CancellationToken. The idea is that we will cancel the new token when the timeout has been reached. If that's the case, we can catch the OperationCanceledException, and then we're sure that we're timed out. If not, cancellation happens through some other means, which means it wasn't a timeout that caused the exception. 

            //LinkedTokenSource will ensure that when your original CancellationToken reaches a canceled state, the new one will get canceled as well. So by doing that, cancellation still works. We cancel that token source after the provided _timeOut value. 
            using (var linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                linkedCancellationTokenSource.CancelAfter(_timeOut);
                try
                {
                    return await base.SendAsync(request, linkedCancellationTokenSource.Token);
                }
                catch (OperationCanceledException ex)
                {
                    if (!cancellationToken.IsCancellationRequested) //if the operation wasn't canceled..
                    {
                        throw new TimeoutException("The request timed out", ex);
                    }
                    throw;
                }
            }
        }

    }
}
