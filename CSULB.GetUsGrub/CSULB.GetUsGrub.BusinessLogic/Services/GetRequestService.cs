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
        private string _url { get; set; }

        public GetRequestService(string url)
        {
            _url = url;
        }

        public async Task<HttpResponseMessage> Execute()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync(_url);
            }
        }
    }
}
