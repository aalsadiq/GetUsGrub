using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantProfile</c> class.
    /// Defines properties pertaining to user account.
    /// <para>
    /// @author: Andrew Kao
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.RestaurantProfile")]
    public class RestaurantProfile : IRestaurantProfile, IEntity
    {
        public RestaurantProfile(
            int? id, string phoneNumber, Address address, 
            RestaurantDetail details, double latitude, double longitude)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Address = address;
            Details = details;
            Latitude = latitude;
            Longitude = longitude;
        }

        public RestaurantProfile(
            string phoneNumber, Address address,
            RestaurantDetail details, double latitude, double longitude)
        {
            PhoneNumber = phoneNumber;
            Address = address;
            Details = details;
            Latitude = latitude;
            Longitude = longitude;
        }

        [Key]
        [ForeignKey("UserProfile")]
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public RestaurantDetail Details { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // TODO: @Rachel Need to include Food Preference List in RestaurantProfile [-Jenn]

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<RestaurantMenu> RestaurantMenus { get; set; }
        public virtual IList<BusinessHour> BusinessHours { get; set; }
    }
}
