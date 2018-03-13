using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHoursList
    {
        IEnumerable<BusinessHour> BusinessHoursList { get; set; }
    }
}
