using System;
using System.Collections.Generic;
using System.Text;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Interface representing restaurant information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public interface IRestaurantProfile
    {
        IList<IRestaurantMenu> Menus { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }
    }
}