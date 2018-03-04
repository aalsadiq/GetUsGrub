using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface IBusinessHours
    {
        IEnumerable<BusinessHour> BusinessHours { get; set; }
    }
}
