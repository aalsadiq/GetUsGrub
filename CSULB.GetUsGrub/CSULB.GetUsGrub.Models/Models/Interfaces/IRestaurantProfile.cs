using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The IRestaurantProfile interface.
    /// A contract with defined properties for the RestaurantProfile class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public interface IRestaurantProfile
    {
        IList<BusinessHour> BusinessHoursList { get; set; }
        string PhoneNumber { get; set; }
        Address Address { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
    }
}
