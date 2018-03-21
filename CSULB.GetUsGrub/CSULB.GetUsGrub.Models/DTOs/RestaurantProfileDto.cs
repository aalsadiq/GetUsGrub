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
				//TODO: @andrew remove the restaurantmenuitems
				public RestaurantProfileDto(UserProfile userProfile, RestaurantProfile restaurantProfile, IList<BusinessHour> businessHours,
						Dictionary<RestaurantMenu, IList<RestaurantMenuItem>> menuDictionary)
				{
						DisplayName = userProfile.DisplayName;
						DisplayPicture = userProfile.DisplayPicture;
						PhoneNumber = restaurantProfile.PhoneNumber;
						Address = restaurantProfile.Address;
						Details = restaurantProfile.Details;
						Latitude = restaurantProfile.Latitude;
						Longitude = restaurantProfile.Longitude;
						BusinessHours = businessHours;
						MenuDictionary = menuDictionary;
				}
				public string Username { get; set; }
				public string DisplayName { get; set; }
				public string DisplayPicture { get; set; }
				public string PhoneNumber { get; set; }
				public Address Address { get; set; }
				public RestaurantDetail Details { get; set; }
				public double Latitude { get; set; }
				public double Longitude { get; set; }
				public Dictionary<RestaurantMenu, IList<RestaurantMenuItem>> MenuDictionary;
				public IList<BusinessHour> BusinessHours { get; set; }
				// TODO: @jenn why are menus and menu items collections instead of lists
		}
}