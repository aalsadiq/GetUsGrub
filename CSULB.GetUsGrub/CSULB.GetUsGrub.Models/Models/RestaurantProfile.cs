using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantProfile</c> class.
    /// Defines properties pertaining to user account.
    /// <para>
    /// @author: Andrew Kao, Brian Fann
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.RestaurantProfile")]
    public class RestaurantProfile : IRestaurantProfile, IEntity//Maybe Remove Profile...
    {
        // Automatic Properties
        [Key]
        [ForeignKey("UserProfile")]
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public RestaurantDetail Details { get; set; }//ASK Andrew & Brian
        public DbGeography Location
        {
            get
            {
                int srid = 4326;
                string wkt = $"POINT({GeoCoordinates.Longitude} {GeoCoordinates.Latitude})";

                return DbGeography.PointFromText(wkt, srid);
            }
            set
            {
                GeoCoordinates.Latitude = value.Latitude ?? 0;
                GeoCoordinates.Latitude = value.Longitude ?? 0;
            }
        }
        [NotMapped]
        public GeoCoordinates GeoCoordinates { get; set; }

        // TODO: @Rachel Need to include Food Preference List in RestaurantProfile [-Jenn]

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
        public virtual IList<BusinessHour> BusinessHours { get; set; }

        // Constructors
        public RestaurantProfile() { }

        public RestaurantProfile(string phoneNumber, Address address, RestaurantDetail details, double? latitude, double? longitude)
        {
            PhoneNumber = phoneNumber;
            Address = address;
            Details = details;
            GeoCoordinates.Latitude = latitude;
            GeoCoordinates.Longitude = longitude;
        }

        public RestaurantProfile(int? id, string phoneNumber, Address address, RestaurantDetail details, double latitude, double longitude)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Address = address;
            Details = details;
            GeoCoordinates.Latitude = latitude;
            GeoCoordinates.Longitude = longitude;
        }

        public RestaurantProfile(string phoneNumber, Address address, RestaurantDetail details)
        {
            PhoneNumber = phoneNumber;
            Address = address;
            Details = details;
        }
    }
}
