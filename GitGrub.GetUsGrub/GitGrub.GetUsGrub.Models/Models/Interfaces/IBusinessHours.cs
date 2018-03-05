using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    public interface IBusinessHours
    {
        [Required]
        IList<BusinessHour> BusinessHours { get; set; }
    }
}
