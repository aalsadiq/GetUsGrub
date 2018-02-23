using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface ILocation
    {
        IEnumerable<Location> Address { get; set; }
    }
}
