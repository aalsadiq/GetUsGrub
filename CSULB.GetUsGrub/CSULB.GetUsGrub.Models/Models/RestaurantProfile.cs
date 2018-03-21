using Newtonsoft.Json;
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
    public class RestaurantProfile : IProfile, IRestaurantProfile, IEntity
    {
        [Key]
        [ForeignKey("UserProfile")]
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public IRestaurantDetail Details { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IList<IRestaurantMenu> Menus { get; set; }
        // TODO: @Andrew Why is display name here when it is already in UserProfile? [-Jenn]
        public string DisplayName { get; set; }
        public string DisplayPicture { get; set; }

        
        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
        public virtual ICollection<BusinessHour> BusinessHours { get; set; }
    }
}
