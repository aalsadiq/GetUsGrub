using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu item class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    [Table("GetUsGrub.RestaurantMenuItems")]
    public class RestaurantMenuItem : IMenuItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("GetUsGrub.Menus")]
        public int MenuId { get; set; } 

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string ItemPicture { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}