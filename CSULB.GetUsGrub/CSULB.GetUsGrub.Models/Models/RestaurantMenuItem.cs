using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu item class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    [Table("GetUsGrub.RestaurantMenuItem")]
    public class RestaurantMenuItem : IMenuItem, IEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RestaurantMenu")]
        public int MenuId { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string ItemPicture { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        // Navigation Properties
        public virtual RestaurantMenu RestaurantMenu { get; set; }
    }
}