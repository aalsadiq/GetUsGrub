using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant profile interface
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    public interface IRestaurantProfile
    {
        //TODO: add food preferences, price range, food type
        string RestaurantName { get; set; }

        string City { get; set; }

        string State { get; set; }

        int ZipCode { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }

        double PhoneNumber { get; set; }

        IList<IRestaurantMenu> Menus { get; set; }

        IEnumerable<IBusinessHour> BusinessHours { get; set; }

        string RestaurantType { get; set; } //Restaurant type renamed to food type
    }
}
