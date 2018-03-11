using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHoursList
    {
        IEnumerable<BusinessHours> BusinessHoursList { get; set; }
    }
}
