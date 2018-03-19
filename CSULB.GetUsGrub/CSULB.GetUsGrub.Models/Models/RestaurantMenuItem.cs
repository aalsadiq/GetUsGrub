using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Menu item class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/15/18
    /// </summary>

    [Table("GetUsGrub.RestaurantMenuItems")]
    public class RestaurantMenuItem : IMenuItem
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.RestaurantMenus")]
        public int? MenuId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public double ItemPrice { get; set; }

        public string ItemPicture { get; set; }

        [Required]
        public string ItemType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public RestaurantMenuItem(string name, double price, string picture, string type, string description, bool isActive)
        {
            ItemName = name;
            ItemPrice = price;
            ItemPicture = picture;
            ItemType = type;
            Description = description;
            IsActive = isActive;
        }
    }
}
