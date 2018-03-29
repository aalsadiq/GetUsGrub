using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu item class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/20/18
    /// </summary>

    [Table("GetUsGrub.RestaurantMenuItem")]
    public class RestaurantMenuItem : IMenuItem
    {
        public RestaurantMenuItem() { }

        public RestaurantMenuItem(string itemName, decimal itemPrice, string itemPicture, string tag, string description, bool isActive)
        {
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            IsActive = isActive;
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

        [Key]
        public int? Id { get; set; }

        [ForeignKey("RestaurantMenu")]
        public int? MenuId { get; set; }

        [Required]
        public string ItemName { get; set; }

        public decimal ItemPrice { get; set; }

        public string ItemPicture { get; set; }

        [Required]
        public string Tag { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        //Navigation Properties
        public virtual RestaurantMenu RestaurantMenu { get;set; }
    }
}