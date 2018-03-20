using CSULB.GetUsGrub.Models;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Asynchronous service that converts an address into geocoordinates.
    /// </summary>
    public interface IGeocodeServiceAsync
    {
        Task<IGeoCoordinates> GeocodeAddressAsync(IAddress address);
    }
}
