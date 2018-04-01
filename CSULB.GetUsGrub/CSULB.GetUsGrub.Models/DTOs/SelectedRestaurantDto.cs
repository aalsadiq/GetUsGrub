using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this [-Jenn]
    public class SelectedRestaurantDto
    {
        // Automatic Properties
        public GeoCoordinates GeoCoordinates { get; set; }
        public string DisplayName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public IList<BusinessHourDto> BusinessHourDtos { get; set; }

        // Constructors
        public SelectedRestaurantDto() { }

        public SelectedRestaurantDto(double latitude, double longitude, string displayName, Address address,
            string phoneNumber, IList<BusinessHourDto> businessHourDtos)
        {
            GeoCoordinates = new GeoCoordinates(latitude: latitude, longitude: longitude);
            DisplayName = displayName;
            Address = address;
            PhoneNumber = phoneNumber;
            BusinessHourDtos = businessHourDtos;
        }
    }
}
