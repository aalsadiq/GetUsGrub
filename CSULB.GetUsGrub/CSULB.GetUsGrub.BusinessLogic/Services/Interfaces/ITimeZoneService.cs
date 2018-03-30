using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for a synchronous service that provides the offset (in seconds) from UTC.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/29/18
    /// </summary>
    public interface ITimeZoneService
    {
        /// <summary>
        /// Calculates offset from UTC from geocoordinates.
        /// </summary>
        /// <param name="coordinates">Coordinates of location to check time zone.</param>
        /// <returns>Offset of time (in seconds) from UTC.</returns>
        ResponseDto<int> GetOffset(IGeoCoordinates coordinates);
    }
}
