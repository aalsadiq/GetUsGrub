using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant profile interface
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public interface IRestaurantProfile
    {
        //TODO: add food preferences and price range
        string RestaurantName { get; set; }

        Address Address { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }

        double PhoneNumber { get; set; }

        IList<IRestaurantMenu> Menus { get; set; }

        IList<IBusinessHour> BusinessHours { get; set; }

        string FoodType { get; set; }
    }
}
