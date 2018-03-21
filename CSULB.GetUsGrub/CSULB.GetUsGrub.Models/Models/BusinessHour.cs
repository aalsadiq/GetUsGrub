using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>BusinessHour</c> class.
    /// Defines properties pertaining to a business hour.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class BusinessHour
    {
        [Required]
        public string Day { get; set; }
        [Required]
        public string OpenTime { get; set; }
        [Required]
        public string CloseTime { get; set; }
    }
}
