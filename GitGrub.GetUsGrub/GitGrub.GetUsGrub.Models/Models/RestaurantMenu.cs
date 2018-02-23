using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public class RestaurantMenu : IRestaurantMenu
    {
        public int Id { get; set; }

        public string MenuName { get; set; }

        public IEnumerable<IMenuItem> Items { get; set; }
    }
}
