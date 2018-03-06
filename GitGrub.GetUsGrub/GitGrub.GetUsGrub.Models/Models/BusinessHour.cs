using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>BusinessHour</c> class.
    /// Defines properties pertaining to business hours in a day.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class BusinessHour
    {
        // TODO: Validate that it is in military time
        [Required]
        public string Day { get; set; }

        [Required]
        public string OpenTime { get; set; }

        [Required]
        public string CloseTime { get; set; }
    }
}