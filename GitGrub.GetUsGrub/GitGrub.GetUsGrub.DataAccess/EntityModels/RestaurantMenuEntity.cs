using GitGrub.GetUsGrub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// A duplicate restaurant menu entity to abstract the ORM framework.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.RestaurantMenu")]
    public class RestaurantMenuEntity : IRestaurantMenu
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        public string MenuName { get; set; }

        public IEnumerable<IMenuItem> Items { get; set; }

        // NAVIGATION
        public UserAccountEntity UserAccount { get; set; }
    }
}
