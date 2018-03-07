using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The IBusinessHours interface.
    /// A contract with defined property as list of BusinessHours.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface IBusinessHours
    {
        [Required]
        IList<BusinessHour> BusinessHours { get; set; }
    }
}
