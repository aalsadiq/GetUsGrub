using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public class RestaurantMenu : IRestaurantMenu
    {
        /// <summary>
        /// Restaurant menu class
        /// 
        /// Author: Andrew Kao
        /// Last Updated: 2/22/18
        /// </summary>
        public string MenuName { get; set; }
        public IEnumerable<IMenuItem> Items { get; set; }
    }
}