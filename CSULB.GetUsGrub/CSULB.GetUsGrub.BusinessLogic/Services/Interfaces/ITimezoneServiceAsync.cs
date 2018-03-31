using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for an asynchronous service that provides the offset (in seconds) from UTC.
    /// 
    /// @Author: Brian Fann
    /// @Last Update: 3/29/18
    /// </summary>
    public interface ITimeZoneServiceAsync
    {
        /// <summary>
        /// Calculates offset from UTC from geocoordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates of location to check time zone.</param>
        /// <returns>Offset of time (in seconds) from UTC.</returns>
        Task<ResponseDto<int>> GetOffsetAsync(IGeoCoordinates coordinates);
    }
}
