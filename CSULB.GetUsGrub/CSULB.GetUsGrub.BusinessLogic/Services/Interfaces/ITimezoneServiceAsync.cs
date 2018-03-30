using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public interface ITimeZoneServiceAsync
    {
        Task<ResponseDto<int>> GetOffsetAsync(IGeoCoordinates coordinates);
    }
}
