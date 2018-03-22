using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantProfile</c> class.
    /// Defines properties pertaining to user account.
    /// <para>
    /// @author: Andrew Kao, Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.RestaurantProfile")]
    public class RestaurantProfile : IRestaurantProfile, IEntity//Maybe Remove Profile...
    {
        public RestaurantProfile() { }

        // TODO: @Jenn Please parameterize this [-Jenn]
        public RestaurantProfile(RestaurantProfileDto restaurantProfileDto)
        {
            PhoneNumber = restaurantProfileDto.PhoneNumber;
            Address = restaurantProfileDto.Address;
            Details = restaurantProfileDto.Details;
            Latitude = restaurantProfileDto.Latitude;
            Longitude = restaurantProfileDto.Longitude;
        }

        [Key]
        [ForeignKey("UserProfile")]
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public RestaurantDetail Details { get; set; }//ASK Andrew & Brian
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // TODO: @Rachel Need to include Food Preference List in RestaurantProfile [-Jenn]
        
        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
        public virtual IList<BusinessHour> BusinessHours { get; set; }
    }
}
