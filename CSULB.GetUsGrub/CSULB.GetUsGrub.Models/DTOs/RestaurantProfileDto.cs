using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant profile DTO
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class RestaurantProfileDto : IRestaurantProfile, IRestaurantDetail
    {
        public string Username { get; set; }

        public string RestaurantName { get; set; }

        public Address Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string PhoneNumber { get; set; }

        public IList<IRestaurantMenu> Menus { get; set; }

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

        // Constructor
        public RestaurantProfileDto(string username, string restaurantName, Address address,
            double latitude, double longitude, string phoneNumber, IList<IRestaurantMenu> menus, 
            IList<IBusinessHour> businessHours, string foodType, bool reservations, bool delivery,
            bool takeOut, bool creditCards, string attire, bool alcohol, bool outdoorSeating, bool tv, 
            bool driveThru, bool caters, bool pets)
        {
            Username = username;
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

        public RestaurantProfileDto(RestaurantProfile restaurantProfile)
        {
            RestaurantName = restaurantProfile.RestaurantName;
            Address = restaurantProfile.Address;
            Latitude = restaurantProfile.Latitude;
            Longitude = restaurantProfile.Longitude;
            PhoneNumber = restaurantProfile.PhoneNumber;
            Menus = restaurantProfile.Menus;
            BusinessHours = restaurantProfile.BusinessHours;
            FoodType = restaurantProfile.FoodType;
            HasReservations = restaurantProfile.HasReservations;
            HasDelivery = restaurantProfile.HasDelivery;
            HasTakeOut = restaurantProfile.HasTakeOut;
            AcceptCreditCards = restaurantProfile.AcceptCreditCards;
            Attire = restaurantProfile.Attire;
            ServesAlcohol = restaurantProfile.ServesAlcohol;
            HasOutdoorSeating = restaurantProfile.HasOutdoorSeating;
            HasTv = restaurantProfile.HasTv;
            HasDriveThru = restaurantProfile.HasDriveThru;
            Caters = restaurantProfile.Caters;
            AllowsPets = restaurantProfile.AllowsPets;
        }
    }
}
