using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant profile class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>

    [Table("GetUsGrub.RestaurantProfiles")]
    public class RestaurantProfile : IRestaurantProfile, IRestaurantDetail
    {
        public RestaurantProfile() { }

        public RestaurantProfile(RestaurantProfileDto restaurantProfileDto)
        {
            PhoneNumber = restaurantProfileDto.PhoneNumber;
            Address = restaurantProfileDto.Address;
            Details = restaurantProfileDto.Details;
            Latitude = restaurantProfileDto.Latitude;
            Longitude = restaurantProfileDto.Longitude;
        }

        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.UserProfiles")]
        public int? UserId { get; set; }

        [Required]
        public string RestaurantName { get; set; }

        [Required]
        public Address Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        // TODO: @Rachel Need to include Food Preference List in RestaurantProfile [-Jenn]
        
        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
        public virtual IList<BusinessHour> BusinessHours { get; set; }
    }
}