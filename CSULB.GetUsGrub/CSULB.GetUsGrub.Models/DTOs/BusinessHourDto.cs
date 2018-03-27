using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>BusinessHourDto</c> class.
    /// Defines properties pertaining to a data transfer object for a business hour.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class BusinessHourDto : IBusinessHour
    {
        [Required]
        public string Day { get; set; }

        [Required]
        public string OpenTime { get; set; }

        [Required]
        public string CloseTime { get; set; }
    }
}
