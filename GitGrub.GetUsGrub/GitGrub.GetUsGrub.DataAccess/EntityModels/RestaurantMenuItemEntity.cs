using GitGrub.GetUsGrub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// A duplicate restaurant menu item entity to abstract the ORM framework.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.RestaurantMenuItem")]
    public class RestaurantMenuItemEntity : IMenuItem
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

        // NAVIGATION
        public RestaurantMenuEntity RestaurantMenu { get; set; }
    }
}