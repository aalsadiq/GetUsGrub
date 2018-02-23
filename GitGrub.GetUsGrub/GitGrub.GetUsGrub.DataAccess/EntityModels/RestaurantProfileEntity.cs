using GitGrub.GetUsGrub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// A duplicate restaurant profile entity to abstract the ORM framework.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.RestaurantProfile")]
    public class RestaurantProfileEntity : IProfile, IRestaurantProfile
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        public IRestaurantDetail Details { get; set; }

        public IList<IRestaurantMenu> Menu { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // NAVIGATION
        public UserAccountEntity UserAccount { get; set; }
    }
}
