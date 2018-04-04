using System.Net.Http;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for a service that sends an outgoing request.
    /// </summary>
    public interface IRequestService
    {
        Task<HttpResponseMessage> Execute();
    }
}
