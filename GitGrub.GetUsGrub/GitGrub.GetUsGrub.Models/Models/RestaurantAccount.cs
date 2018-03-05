using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
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
