using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Service that converts an address into geocoordinates.
    /// </summary>
    public interface IGeocodeService
    {
        IGeoCoordinates GeocodeAddress(IAddress address);
    }
}
