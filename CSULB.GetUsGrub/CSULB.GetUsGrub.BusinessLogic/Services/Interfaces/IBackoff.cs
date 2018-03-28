using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for a request service that implements exponential backoff.
    /// </summary>
    public interface IBackoff
    {
        Task<string> TryExecute();
    }
}
