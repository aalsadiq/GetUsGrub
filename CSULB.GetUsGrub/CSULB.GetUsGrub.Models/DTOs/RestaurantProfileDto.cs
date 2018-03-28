using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant profile DTO
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/20/18
    /// </summary>
    public class RestaurantProfileDto : IRestaurantProfile
    {
        // TODO: @andrew temporary for RestaurantProfileDtoValidatorUnitTests, make unit tests use actual constructor
        public RestaurantProfileDto() { }
        public RestaurantProfileDto(RestaurantProfile restaurantProfile, IList<BusinessHour> businessHours, IList<RestaurantMenu> restaurantMenus, IList<RestaurantMenuItem> restaurantMenuItems)
        {
            PhoneNumber = restaurantProfile.PhoneNumber;
            Address = restaurantProfile.Address;
            Details = restaurantProfile.Details;
            Latitude = restaurantProfile.Latitude;
            Longitude = restaurantProfile.Longitude;
            BusinessHours = businessHours;
            RestaurantMenus = restaurantMenus;
            RestaurantMenuItems = restaurantMenuItems;
        }

        public RestaurantProfileDto(RestaurantProfile restaurantProfile, IList<BusinessHour> businessHours, Dictionary<RestaurantMenu, IList<RestaurantMenuItem>> menuDictionary)
        {
            PhoneNumber = restaurantProfile.PhoneNumber;
            Address = restaurantProfile.Address;
            Details = restaurantProfile.Details;
            Latitude = restaurantProfile.Latitude;
            Longitude = restaurantProfile.Longitude;
            BusinessHours = businessHours;
            MenuDictionary = menuDictionary;
        }

        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public RestaurantDetail Details { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Dictionary<RestaurantMenu, IList<RestaurantMenuItem>> MenuDictionary { get; set; }
        public IList<BusinessHour> BusinessHours { get; set; }
        public IList<RestaurantMenu> RestaurantMenus { get; set; }
        public IList<RestaurantMenuItem> RestaurantMenuItems { get; set; }
        // TODO: @jenn why are menus and menu items collections instead of lists
    }
}