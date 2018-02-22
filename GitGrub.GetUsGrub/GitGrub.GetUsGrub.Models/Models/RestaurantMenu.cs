using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public class RestaurantMenu : IRestaurantMenu
    {
        public IEnumerable<IMenuItem> Items { get; set; }
    }
}
