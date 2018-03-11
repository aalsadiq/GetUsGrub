using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantProfileDto</c> class.
    /// Defines properties pertaining to a data transfer object of a restaurant profile.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileDto
    {
        [Required]
        public IList<BusinessHour> BusinessHours { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public Address Address { get; set; }
    }
}
