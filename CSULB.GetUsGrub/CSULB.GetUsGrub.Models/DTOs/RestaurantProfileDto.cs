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
        
        // Automatic properties
        public string DisplayName { get; set; }
        public string DisplayPicture { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public RestaurantDetail Details { get; set; }
        public GeoCoordinates GeoCoordinates { get; set; }
        public IList<RestaurantMenuWithItems> RestaurantMenusList { get; set; }
        //public Dictionary<RestaurantMenu, IList<RestaurantMenuItem>> menuDictionary
        public IList<BusinessHour> BusinessHours { get; set; }
        //public ICollection<RestaurantMenu> RestaurantMenus { get; set; }
        //public ICollection<RestaurantMenuItem> RestaurantMenuItems { get; set; }

        // Constructors
        public RestaurantProfileDto() { }

        /*public RestaurantProfileDto(RestaurantProfile restaurantProfile, double? latitude, double? longitude, IList<BusinessHour> businessHours, IList<RestaurantMenu> restaurantMenus, IList<RestaurantMenuItem> restaurantMenuItems)
        {
            PhoneNumber = restaurantProfile.PhoneNumber;
            Address = restaurantProfile.Address;
            Details = restaurantProfile.Details;
            GeoCoordinates.Latitude = latitude;
            GeoCoordinates.Longitude = longitude;
            BusinessHours = businessHours;
            RestaurantMenus = restaurantMenus;
            RestaurantMenuItems = restaurantMenuItems;
        }*/

        public RestaurantProfileDto(UserProfile userProfile, RestaurantProfile restaurantProfile, IList<BusinessHour> businessHours, IList<RestaurantMenuWithItems> restaurantMenusList)
        {
            DisplayName = userProfile.DisplayName;
            DisplayPicture = userProfile.DisplayPicture;
            PhoneNumber = restaurantProfile.PhoneNumber;
            Address = restaurantProfile.Address;
            Details = restaurantProfile.Details;
            BusinessHours = businessHours;
            RestaurantMenusList = restaurantMenusList;
        }
    }
}