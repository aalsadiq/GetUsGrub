using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface IRestaurantMenu
    {
        IEnumerable<IMenuItem> Items { get; set; }
    }
}
