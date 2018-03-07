using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantAccount</c> class.
    /// Defines properties pertaining to a restaurant account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class RestaurantAccount : IRestaurantAccount
    {
        [Required]
        [JsonProperty(PropertyName = "BusinessHours")]
        public IList<BusinessHour> BusinessHours { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Address Address { get; set; }
    }
}
