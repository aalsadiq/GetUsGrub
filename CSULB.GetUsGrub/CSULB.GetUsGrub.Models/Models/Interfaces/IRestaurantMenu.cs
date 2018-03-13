using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    public interface IRestaurantMenu
    {
        IEnumerable<IMenuItem> Items { get; set; }
    }
}
