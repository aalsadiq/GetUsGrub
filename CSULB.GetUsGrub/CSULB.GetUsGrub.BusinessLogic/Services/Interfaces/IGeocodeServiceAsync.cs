using CSULB.GetUsGrub.Models;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Interface for an asynchronous service that converts addresses into geocoordinates.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/29/18
    /// </summary>
    public interface IGeocodeServiceAsync
    {
        /// <summary>
        /// Converts an address into geocoordinates.
        /// </summary>
        /// <param name="address">Address to geocode.</param>
        /// <returns>Coordinates of address.</returns>
        Task<ResponseDto<IGeoCoordinates>> GeocodeAsync(IAddress address);
    }
}
