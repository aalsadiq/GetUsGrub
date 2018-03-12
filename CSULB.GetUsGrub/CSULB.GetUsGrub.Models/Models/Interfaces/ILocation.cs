using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    public interface ILocation
    {
        IEnumerable<Location> Address { get; set; }
    }
}
