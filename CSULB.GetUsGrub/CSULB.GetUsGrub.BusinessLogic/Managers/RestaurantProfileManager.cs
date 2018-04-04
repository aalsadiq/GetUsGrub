using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Performs business logic and executes requests regarding restaurant profiles
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class RestaurantProfileManager : IProfileManager<RestaurantProfileDto>
    {
        public ResponseDto<RestaurantProfileDto> GetProfile(string username)
        {
            // Retrieve profile from database
            var profileGateway = new RestaurantProfileGateway();

            var responseDtoFromGateway = profileGateway.GetRestaurantProfileByUsername(username);

            return responseDtoFromGateway;
        }

        public ResponseDto<bool> EditProfile(RestaurantProfileDto restaurantProfileDto)
        {
            // Prelogic validation strategy TODO: @andrew move this to after the 
            var editRestaurantProfilePreLogicValidationStrategy = new EditRestaurantUserProfilePreLogicValidationStrategy(restaurantProfileDto);

            var result = editRestaurantProfilePreLogicValidationStrategy.ExecuteStrategy();

            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = "Something went wrong. Please try again later."
                };
            }

            // Extract DTO contents and map DTO to domain model
   
            var username = restaurantProfileDto.Username;

            // Extract restaurant profile domain
            var restaurantProfileDomain = new RestaurantProfile(
                restaurantProfileDto.PhoneNumber,
                restaurantProfileDto.Address,
                restaurantProfileDto.Details,
                restaurantProfileDto.GeoCoordinates.Latitude,
                restaurantProfileDto.GeoCoordinates.Longitude);

            // Extract business hours domains
            var businessHourDomains = restaurantProfileDto.BusinessHours;


            // Extract restaurant menu
            var restaurantMenuDomains = restaurantProfileDto.RestaurantMenus;

            // Extract menu items
            var restaurantMenuItemDomains = restaurantProfileDto.RestaurantMenuItems;

            // Execute update of database
            var profileGateway = new RestaurantProfileGateway();

            var responseDtoFromGateway = profileGateway.EditRestaurantProfile(username, restaurantProfileDomain, businessHourDomains, restaurantMenuDomains, restaurantMenuItemDomains);

            return responseDtoFromGateway;
        }
    }
}