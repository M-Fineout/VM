using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client
{
    public class RetryPolicyDelegatingHandler : DelegatingHandler
    {
        private readonly int _maximumNumberOfRetries = 3;

        public RetryPolicyDelegatingHandler(int maximumNumberOfRetries) : base()
        {
            _maximumNumberOfRetries = maximumNumberOfRetries;
        }

        public RetryPolicyDelegatingHandler(HttpMessageHandler innerHandler, int maximumNumberOfRetries) : base(innerHandler)
        {
            _maximumNumberOfRetries = maximumNumberOfRetries;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < _maximumNumberOfRetries; i++)
            {
                //get response to validate passing request to next handler in the pipeline
                response = await base.SendAsync(request, cancellationToken);

                //if request is successful, return and pass to next handler
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
            }
            //else return response (failed)
            return response;
        }
    }
}
