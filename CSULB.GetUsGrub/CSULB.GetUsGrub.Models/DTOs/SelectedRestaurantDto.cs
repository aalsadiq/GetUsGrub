using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this [-Jenn]
    public class SelectedRestaurantDto
    {
        public SelectedRestaurantDto(double latitude, double longitude, string displayName, Address address,
            string phoneNumber, IList<BusinessHour> businessHours)
        {
            GeoCoordinates = new GeoCoordinates(latitude: latitude, longitude: longitude);
            DisplayName = displayName;
            Address = address;
            PhoneNumber = phoneNumber;
            BusinessHours = businessHours;
        }

        public GeoCoordinates GeoCoordinates { get; set; }
        public string DisplayName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public IList<BusinessHour> BusinessHours { get; set; }
    }
}
