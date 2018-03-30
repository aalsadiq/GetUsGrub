using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Implementation of BackoffRequest catered to Google's API.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/29/18
    /// </summary>
    public class GoogleBackoffRequest : BackoffRequest
    {
        string _retryStatus { get; set; }

        public GoogleBackoffRequest(IRequestService request, string RetryStatus = "OVER_QUERY_LIMIT", int initialWait = 1000, double waitMultiplier = 1.5, int maxRetries = 5) 
            : base(request, initialWait, waitMultiplier, maxRetries)
        {
            _retryStatus = RetryStatus;
        }

        /// <summary>
        /// Retry the request if the status of the response matches the _retryStatus.
        /// </summary>
        /// <param name="response">Response of the request sent.</param>
        /// <returns>True if retry is needed, False if request is successful.</returns>
        protected override async Task<bool> IsRetryNeeded(HttpResponseMessage response)
        {
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObj = JObject.Parse(responseJson);
            var status = (string)responseObj.SelectToken("status");

            return status.Equals(_retryStatus);
        }
    }
}