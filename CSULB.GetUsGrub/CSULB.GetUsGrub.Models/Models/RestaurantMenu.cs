using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/15/18
    /// </summary>

    [Table("GetUsGrub.RestaurantMenus")]
    public class RestaurantMenu : IRestaurantMenu
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.RestaurantProfiles")]
        public int? RestaurantId { get; set; }

        public string MenuName { get; set; }

        public IEnumerable<IMenuItem> Items { get; set; }

        // Constructor
        public RestaurantMenu(string menuName, IEnumerable<IMenuItem> items)
        {
            MenuName = menuName;
            Items = items;
        }
    }
}
