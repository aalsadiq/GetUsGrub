using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Menu item class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>

    [Table("GetUsGrub.MenuItem")]
    public class MenuItem : IMenuItem
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.RestaurantMenu")]
        public int? MenuId { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public string ItemPicture { get; set; }

        public string ItemType { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
