using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RegisterRestaurantDto</c> class.
    /// Defines properties pertaining to a data transfer object for registering a restaurant.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class RegisterRestaurantDto : RegisterUserDto
    {
        [Required]
        public RestaurantProfileDto RestaurantProfileDto { get; set; }

        [Required]
        public IList<BusinessHourDto> BusinessHourDtos { get; set; }
    }
}
