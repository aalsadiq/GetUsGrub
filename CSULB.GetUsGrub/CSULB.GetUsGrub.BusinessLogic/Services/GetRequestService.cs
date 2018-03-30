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

        public async Task<HttpResponseMessage> Execute()
        {
            // TODO Check to see if there's a way to enable this via configuration rather than with each request [-Brian]
            // Sets Security Protocol to TLS 1.2 -- Required by PwnedPasswords API
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            return await _client.GetAsync(_url);
        }
    }
}
