using CSULB.GetUsGrub.Models;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public interface IGeocodeServiceAsync
    {
        Task<IGeoCoordinates> GeocodeAsync(IAddress address);
    }
}
