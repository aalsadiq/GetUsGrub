using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public interface ITimezoneServiceAsync
    {
        Task<ResponseDto<int>> GetOffsetAsync(IGeoCoordinates coordinates);
    }
}
