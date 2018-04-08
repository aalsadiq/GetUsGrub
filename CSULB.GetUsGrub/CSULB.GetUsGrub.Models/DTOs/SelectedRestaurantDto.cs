using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SelectedRestaurantDto</c> class.
    /// Defines properties pertaining to a data transfer object for a selected restaurant.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/04/2018
    /// </para>
    /// </summary>
    public class SelectedRestaurantDto
    {
        // Automatic Properties
        public GeoCoordinates RestaurantGeoCoordinates { get; set; }
        public GeoCoordinates ClientUserGeoCoordinates { get; set; }
        public string DisplayName { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public IList<BusinessHourDto> BusinessHourDtos { get; set; }
        // TODO: @Andrew Will need to put public id for restaurant here [-Jenn]
    }
}
