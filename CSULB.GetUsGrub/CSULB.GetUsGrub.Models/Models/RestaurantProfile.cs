using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant user profile class
    /// 
    /// Author: Jennifer Nguyen
    /// Last Updated: 3/10/18
    /// </summary>
    [Table("GetUsGrub.RestaurantProfile")]
    public class RestaurantProfile : IProfile, IRestaurantProfile, IEntity
    {
        [ForeignKey("UserProfile")]
        public int Id { get; set; }

        public string ProfileName { get; set; }

        public string ProfilePicture { get; set; }

        public string PhoneNumber { get; set; }
        
        public Address Address { get; set; }

        public RestaurantDetail Details { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string BusinessHoursJson { get; set; }

        public IList<IRestaurantMenu> Menus { get; set; }

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
    }
}
