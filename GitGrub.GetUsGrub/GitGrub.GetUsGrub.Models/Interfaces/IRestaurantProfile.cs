using System.Collections.Generic;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    /// <summary>
    /// Interface representing restaurant information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    interface IRestaurantProfile
    {
        double Longitude { get; set; }
        double Latitude { get; set; }
        double PhoneNumber { get; set; }
        string Category { get; set; }
        IEnumerable<BusinessHours> BusinessSchedule { get; set; }
        List<RestaurantMenu> Menus { get; set; }
    }
}