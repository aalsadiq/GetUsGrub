using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Interface representing restaurant information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    public interface IRestaurantProfile
    {
        IList<IRestaurantMenu> Menus { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }

        IEnumerable<BusinessHours> BusinessSchedule { get; set; }

        string Category { get; set; }

        double PhoneNumber { get; set; }
    }
}