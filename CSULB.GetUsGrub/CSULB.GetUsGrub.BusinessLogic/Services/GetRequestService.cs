using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Service for sending a get request to a specified URL
    /// 
    /// @ Author: Brian Fann
    /// @ Last Updated: 3/22/18
    /// </summary>
    public class GetRequestService : IRequestService
    {
        private static readonly HttpClient _client = new HttpClient();
        private string _url { get; set; }

        public GetRequestService(string url)
        {
            _url = url;
        }

        public async Task<string> Execute()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            return await _client.GetStringAsync(_url);
        }
    }
}
