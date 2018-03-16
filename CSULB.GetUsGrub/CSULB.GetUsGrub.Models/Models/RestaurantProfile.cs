using CSULB.GetUsGrub.Models.Models;
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
    public class RestaurantProfile : IRestaurantProfile, IEntity//Maybe Remove Profile...
    {
        [Key]
        [ForeignKey("UserProfile")]
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public RestaurantDetail Details { get; set; }//ASK Andrew & Brian
        public double Latitude { get; set; }
        public double Longitude { get; set; }
   
        [NotMapped]//anything with not mapped is not in database 
        public IList<BusinessHour> BusinessHours { get; set; }

        public string BusinessHoursJson//inside database
        {
            get => JsonConvert.SerializeObject(BusinessHours);
            set => BusinessHours = JsonConvert.DeserializeObject<List<BusinessHour>>(value);
        }

        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }
    }
}
