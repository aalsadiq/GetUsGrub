using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for a synchronous service that converts addresses into geocoordinates.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/29/18
    /// </summary>
    public interface IGeocodeService
    {
        /// <summary>
        /// Converts an address into geocoordinates.
        /// </summary>
        /// <param name="address">Address to geocode.</param>
        /// <returns>Coordinates of address.</returns>
        ResponseDto<IGeoCoordinates> Geocode(IAddress address);
    }
}
