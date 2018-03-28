using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Get request for Google API's implementing exponential backoff.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/22/18
    /// </summary>
    public class GoogleBackoffGetRequest : GetRequestService, IBackoff
    {
        private string _url { get; set; }
        private string _backoffStatus { get; set; }
        private int _initialWait { get; set; }
        private double _waitMultiplier { get; set; }
        private int _maxRetries { get; set; }
        
        /// <summary>
        /// Constructor with optional parameters.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="backoffStatus">Status code to retry request on.</param>
        /// <param name="initialWait">Initial wait time for first retry.</param>
        /// <param name="waitMultiplier">Factor multiplied against wait time with each iteration.</param>
        /// <param name="maxRetries">Max number of retries allowed.</param>
        public GoogleBackoffGetRequest(string url, string backoffStatus = "", int initialWait = 1000, double waitMultiplier = 1.5, int maxRetries = 5) : base(url)
        {
            _backoffStatus = backoffStatus;
            _initialWait = initialWait;
            _waitMultiplier = waitMultiplier;
            _maxRetries = maxRetries;
        }

        /// <summary>
        /// Attempt to execute a get request multiple times if the resulting status matches the backoff status.
        /// </summary>
        /// <returns>Response of get request.</returns>
        public async Task<string> TryExecute()
        {
            var responseJson = "";

            // Continue sending request until it either does not have the back off status code
            // or it reaches max retry count. Each consecutive iteration has an exponential wait.
            for (var i = 0; i <= _maxRetries; i++)
            {
                responseJson = await Execute();
                var responseObj = JObject.Parse(responseJson);
                var status = (string)responseObj.SelectToken("status");

                // Exit early if not backing off.
                if (!status.Equals(_backoffStatus))
                {
                    return responseJson;
                }

                var waitTime = (int)(_initialWait + _waitMultiplier * i);

                await Task.Delay(waitTime);
            }

            // If request is not able to successfully send after max retry count, return response.
            return responseJson;
        }
    }
}
