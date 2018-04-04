using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this [-Jenn]
    public class SelectedRestaurantDto
    {
        // Automatic Properties
        public double? restaurantLatitude { get; set; }
        public double? restaurantLongitude { get; set; }
        public string DisplayName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public IList<BusinessHourDto> BusinessHourDtos { get; set; }
    }
}
