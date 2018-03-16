using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant profile class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/15/18
    /// </summary>

    [Table("GetUsGrub.RestaurantProfiles")]
    public class RestaurantProfile : IRestaurantProfile, IRestaurantDetail
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("GetUsGrub.UserProfiles")]
        public int? UserId { get; set; }

        public string RestaurantName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int ZipCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double PhoneNumber { get; set; }

        public IList<IRestaurantMenu> Menus { get; set; }

        public IEnumerable<IBusinessHour> BusinessHours { get; set; }

        public string RestaurantType { get; set; }

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

        // Constructor
        public RestaurantProfile(string restaurantName, string city, string state, int zipCode, double latitude, double longitude, double phoneNumber, IList<IRestaurantMenu> menus, IEnumerable<IBusinessHour> businessHours, string restaurantType, bool reservations, bool delivery, bool takeOut, bool creditCards, string attire, bool alcohol, bool outdoorSeating, bool tv, bool driveThru, bool caters, bool pets)
        {
            RestaurantName = restaurantName;
            City = city;
            State = state;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
            PhoneNumber = phoneNumber;
            Menus = menus;
            BusinessHours = businessHours;
            RestaurantType = restaurantType;
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

        public RestaurantProfile()
        {
            RestaurantName = null;
            City = null;
            State = null;
            ZipCode = 0;
            Latitude = 0;
            Longitude = 0;
            PhoneNumber = 0;
            Menus = null;
            BusinessHours = null;
            RestaurantType = null;
            HasReservations = false;
            HasDelivery = false;
            HasTakeOut = false;
            AcceptCreditCards = false;
            Attire = null;
            ServesAlcohol = false;
            HasOutdoorSeating = false;
            HasTv = false;
            HasDriveThru = false;
            Caters = false;
            AllowsPets = false;
        }
    }
}
