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
        public RestaurantMenu(string menuName)
        {
            MenuName = menuName;
        }

        public RestaurantMenu(int? id, int? restaurantId, string menuName)
        {
            Id = id;
            RestaurantId = restaurantId;
            MenuName = menuName;
        }

        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.RestaurantProfiles")]
        public int? RestaurantId { get; set; }

        [Required]
        public string MenuName { get; set; }

        // Navigation Properties
        public virtual RestaurantProfile RestaurantProfile { get; set; }

        public virtual ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }
    }
}
