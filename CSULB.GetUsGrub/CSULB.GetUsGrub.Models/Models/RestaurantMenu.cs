using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>

    [Table("GetUsGrub.Menu")]
    public class RestaurantMenu : IRestaurantMenu
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.RestaurantProfile")]
        public int? RestaurantId { get; set; }

        public string MenuName { get; set; }

        public IEnumerable<IMenuItem> Items { get; set; }
    }
}
