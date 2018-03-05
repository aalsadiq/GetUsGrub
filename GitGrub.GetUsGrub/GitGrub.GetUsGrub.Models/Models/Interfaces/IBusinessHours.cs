using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface IBusinessHours
    {
        IList<BusinessHour> BusinessHours { get; set; }
    }
}
