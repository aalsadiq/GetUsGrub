using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations? [-Jenn]
    /// <summary>
    /// The <c>RestaurantProfile</c> class.
    /// Defines properties pertaining to user account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class RestaurantProfile : IRestaurantProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        // TODO: @Jenn Why did you pick a list? [-Jenn]
        public IList<BusinessHour> BusinessHours { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
