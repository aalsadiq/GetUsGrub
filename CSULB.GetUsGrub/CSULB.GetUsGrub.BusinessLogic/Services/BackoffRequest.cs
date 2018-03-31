using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// A wrapper for a request to implement exponential backoff.
    /// </summary>
    public class BackoffRequest : IBackoff
    {
        IRequestService _request { get; set; }
        int _initialWait { get; set; }
        double _waitMultiplier { get; set; }
        int _maxRetries { get; set; }
        
        public BackoffRequest(IRequestService request, int initialWait = 1000, double waitMultiplier = 1.5, int maxRetries = 5)
        {
            _request = request;
            _initialWait = initialWait;
            _waitMultiplier = waitMultiplier;
            _maxRetries = maxRetries;

            if (_maxRetries < 1)
            {
                _maxRetries = 1;
            }
        }

        /// <summary>
        /// Condition of the response in which the request should be retried.
        /// </summary>
        /// <param name="response">Response of the request sent.</param>
        /// <returns>True if retry is needed, False if request is successful.</returns>
        protected virtual async Task<bool> IsRetryNeeded(HttpResponseMessage response)
        {
            return await Task.Run(() => response.StatusCode != HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> TryExecute()
        {
            HttpResponseMessage response = null;

            // Each iteration sends the request and, if not successful, waits for a period of time.
            for (int i = 0; i < _maxRetries; i++)
            {
                response = await _request.Execute();

                // If successful, returns the response.
                if (!await(IsRetryNeeded(response))) return response;

                // Waits for a period of time. Increases exponentially with each failed request.
                var waitTime = (int)(_initialWait + _waitMultiplier * i);

                await Task.Delay(waitTime);
            }

            return response;
        }
    }
}
