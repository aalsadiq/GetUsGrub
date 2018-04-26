using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu item class
    /// 
    /// @author: Andrew Kao
    /// @updated: 4/5/18
    /// </summary>
    
    public enum Flag
    {
        NotSet = 0,
        Add = 1,
        Edit = 2,
        Delete = 3
    }

    [Table("GetUsGrub.RestaurantMenuItem")]
    public class RestaurantMenuItem : IMenuItem
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        [ForeignKey("RestaurantMenu")]
        public int? MenuId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public decimal ItemPrice { get; set; }
        public string ItemPicture { get; set; }
        [Required]
        public string Tag { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public Flag Flag { get; set; }

        // Navigation Properties
        public virtual RestaurantMenu RestaurantMenu { get; set; }

        // Constructors
        public RestaurantMenuItem() { }

        public RestaurantMenuItem(int? id, string itemName, decimal itemPrice, string itemPicture, string tag, string description, bool isActive, Flag flag)
        {
            Id = id;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            IsActive = isActive;
            Flag = flag;
        }

        public RestaurantMenuItem(int? id, int? menuId, string itemName, decimal itemPrice, string itemPicture, string tag, string description, bool isActive)
        {
            Id = id;
            MenuId = menuId;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            IsActive = isActive;
        }

        public RestaurantMenuItem(int? id, string itemName, decimal itemPrice, string itemPicture, string tag, string description, bool isActive)
        {
            Id = id;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            IsActive = isActive;
        }

        public RestaurantMenuItem(string itemName, decimal itemPrice, string itemPicture, string tag, string description, bool isActive, Flag flag)
        {
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            IsActive = isActive;
            Flag = flag;
        }
    }
}