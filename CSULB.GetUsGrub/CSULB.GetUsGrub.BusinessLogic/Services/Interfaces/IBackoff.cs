using System.Threading.Tasks;
using System.Net.Http;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for a request service that implements exponential backoff.
    /// </summary>
    public interface IBackoff
    {
        Task<HttpResponseMessage> TryExecute();
    }
}
