using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public interface ITimezoneServiceAsync
    {
        Task<int?> GetOffsetAsync(IGeoCoordinates coordinates);
    }
}
