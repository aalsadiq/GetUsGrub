using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface IRestaurantMenu
    {
        /// <summary>
        /// Interface representing a restaurant menu
        /// 
        /// Author: Andrew Kao
        /// Last Updated: 2/22/18
        /// </summary>
        string MenuName { get; set; }
        IEnumerable<IMenuItem> Items { get; set; }
    }
}
