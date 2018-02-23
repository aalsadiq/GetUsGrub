using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface IBusinessHoursList
    {
        IEnumerable<BusinessHours> BusinessHoursList { get; set; }
    }
}
