using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantProfile</c> class.
    /// Defines properties pertaining to user account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.RestaurantProfile")]
    public class RestaurantProfile : IProfile, IRestaurantProfile, IEntity
    {
        [ForeignKey("UserProfile")]
        public int? Id { get; set; }
        // TODO: @Jenn Why did you pick a list? [-Jenn]
        public IList<BusinessHour> BusinessHours { get; set; }
        public string PhoneNumber { get; set; }
        
        public Address Address { get; set; }

        public IRestaurantDetail Details { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IList<IRestaurantMenu> Menus { get; set; }

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
        public IList<BusinessHour> BusinessHoursList { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
