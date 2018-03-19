using Newtonsoft.Json;
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

        [Required]
        public double PhoneNumber { get; set; }

        public IList<IRestaurantMenu> Menus { get; set; }

        [NotMapped]
        public IList<IBusinessHour> BusinessHours { get; set; }

        public string FoodType { get; set; }

        public bool? HasReservations { get; set; }

        public bool? HasDelivery { get; set; }

        public bool? HasTakeOut { get; set; }

        public bool? AcceptCreditCards { get; set; }

        public string Attire { get; set; }

        public bool? ServesAlcohol { get; set; }

        public bool? HasOutdoorSeating { get; set; }

        public bool? HasTv { get; set; }

        public bool? HasDriveThru { get; set; }

        public bool? Caters { get; set; }

        public bool? AllowsPets { get; set; }

        public string BusinessHoursJson
        {
            get => JsonConvert.SerializeObject(BusinessHours);
            set => BusinessHours = JsonConvert.DeserializeObject<IList<IBusinessHour>>(value);
        }
        // Navigation Properties
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<RestaurantMenu> RestaurantMenu { get; set; }

        // Constructor
        public RestaurantProfile(string restaurantName, Address address, 
            double latitude, double longitude, double phoneNumber, IList<IRestaurantMenu> menus, 
            IList<IBusinessHour> businessHours, string foodType, bool reservations, 
            bool delivery, bool takeOut, bool creditCards, string attire, bool alcohol, 
            bool outdoorSeating, bool tv, bool driveThru, bool caters, bool pets)
        {
            RestaurantName = restaurantName;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            PhoneNumber = phoneNumber;
            Menus = menus;
            BusinessHours = businessHours;
            FoodType = foodType;
            HasReservations = reservations;
            HasDelivery = delivery;
            HasTakeOut = takeOut;
            AcceptCreditCards = creditCards;
            Attire = attire;
            ServesAlcohol = alcohol;
            HasOutdoorSeating = outdoorSeating;
            HasTv = tv;
            HasDriveThru = driveThru;
            Caters = caters;
            AllowsPets = pets;
        }
    }
}
