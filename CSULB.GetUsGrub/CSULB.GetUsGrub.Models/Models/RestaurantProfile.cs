using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant user profile class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    [Table("GetUsGrub.RestaurantProfile")]
    public class RestaurantProfile : IProfile, IRestaurantProfile, IEntity
    {
        [ForeignKey("UserProfile")]
        public int Id { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        public IRestaurantDetail Details { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IList<IRestaurantMenu> Menus { get; set; }

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
    }
}
