using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models.Models
{
    /// <summary>
    /// Restaurant menu containing a list of menu items
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    public class RestaurantMenu
    {
        public string MenuName;
        public List<RestaurantMenuItem> Menu;
    }
}
