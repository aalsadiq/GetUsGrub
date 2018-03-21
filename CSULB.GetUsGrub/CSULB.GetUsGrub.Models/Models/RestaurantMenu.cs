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
        public RestaurantMenu(int publicMenuId, string menuName, bool isActive)
        {
            PublicMenuId = publicMenuId;
            MenuName = menuName;
            IsActive = isActive;
        }

        public RestaurantMenu(int? id, int? restaurantId, int publicMenuId, string menuName, bool isActive)
        {
            Id = id;
            RestaurantId = restaurantId;
            PublicMenuId = publicMenuId;
            MenuName = menuName;
            IsActive = isActive;
        }

        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.RestaurantProfiles")]
        public int? RestaurantId { get; set; }

        [Required]
        public int PublicMenuId { get; set; }

        [Required]
        public string MenuName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation Properties
        public virtual RestaurantProfile RestaurantProfile { get; set; }

        public virtual ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }

    }
}
