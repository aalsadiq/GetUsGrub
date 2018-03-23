using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    [Table("GetUsGrub.RestaurantMenu")]
    public class RestaurantMenu : IRestaurantMenu, IEntity
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("RestaurantProfile")]
        public int? RestaurantId { get; set; }

        public string MenuName { get; set; }

        // Navigation Properties
        public virtual RestaurantProfile RestaurantProfile { get; set; }

        public virtual ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }
    }
}
