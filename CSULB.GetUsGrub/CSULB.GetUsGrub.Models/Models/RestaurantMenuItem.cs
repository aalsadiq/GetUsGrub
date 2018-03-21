using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Menu item class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/20/18
    /// </summary>

    [Table("GetUsGrub.RestaurantMenuItems")]
    public class RestaurantMenuItem : IMenuItem
    {
        public RestaurantMenuItem(int publicItemId, string itemName, decimal itemPrice, string itemPicture, string tag, string description, bool isActive)
        {
            PublicItemId = publicItemId;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            IsActive = isActive;
        }

        public RestaurantMenuItem(int? id, int? menuId, int publicItemId, string itemName, decimal itemPrice, string itemPicture, string tag, string description, bool isActive)
        {
            Id = id;
            MenuId = menuId;
            PublicItemId = publicItemId;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicture = itemPicture;
            Tag = tag;
            Description = description;
            IsActive = isActive;
        }

        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.RestaurantMenus")]
        public int? MenuId { get; set; }

        [Required]
        public int PublicItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ItemPrice { get; set; }

        public string ItemPicture { get; set; }

        [Required]
        public string Tag { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
