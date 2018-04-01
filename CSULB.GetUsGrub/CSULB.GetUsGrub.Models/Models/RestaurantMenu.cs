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
    [Table("GetUsGrub.RestaurantMenu")]
    public class RestaurantMenu : IRestaurantMenu
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        [ForeignKey("RestaurantProfile")]
        public int? RestaurantId { get; set; }
        [Required]
        public string MenuName { get; set; }
        [Required]
        public bool IsActive { get; set; }

        // Navigation Properties
        public virtual RestaurantProfile RestaurantProfile { get; set; }
        public virtual ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }

        // Constructors
        public RestaurantMenu() { }

        public RestaurantMenu(string menuName, bool isActive)
        {
            MenuName = menuName;
            IsActive = isActive;
        }

        public RestaurantMenu(int? id, int? restaurantId, string menuName, bool isActive)
        {
            Id = id;
            RestaurantId = restaurantId;
            MenuName = menuName;
            IsActive = isActive;
        }


    }
}
