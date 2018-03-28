using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for a service that sends an out-going request.
    /// </summary>
    public interface IRequestService
    {
        Task<string> Execute();
    }
}
