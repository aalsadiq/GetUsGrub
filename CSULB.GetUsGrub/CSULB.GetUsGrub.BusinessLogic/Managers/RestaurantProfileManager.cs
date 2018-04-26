﻿using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Configuration;
using System.IO;
using System.Web;

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
        public ResponseDto<RestaurantProfileDto> GetProfile(string token)
        {
            // Retrieve account by username
            var userGateway = new UserGateway();

            var tokenService = new TokenService();

            var userAccountResponseDto = userGateway.GetUserByUsername(tokenService.GetTokenUsername(token));

            // Retrieve restaurant profile from database
            var restaurantProfileGateway = new RestaurantProfileGateway();

            var restaurantProfileResponseDto = restaurantProfileGateway.GetRestaurantProfileById(userAccountResponseDto.Data.Id);

            return restaurantProfileResponseDto;
        }

        public ResponseDto<bool> EditProfile(RestaurantProfileDto restaurantProfileDto, string token)
        {
            var geocodeService = new GoogleGeocodeService();

            var editRestaurantProfilePreLogicValidationStrategy = new EditRestaurantUserProfilePreLogicValidationStrategy(restaurantProfileDto);

            var result = editRestaurantProfilePreLogicValidationStrategy.ExecuteStrategy();

            if (result.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Retrieve account by username
            var userGateway = new UserGateway();

            var tokenService = new TokenService();

            var userAccountResponseDto = userGateway.GetUserByUsername(tokenService.GetTokenUsername(token));

            // Extrant user profile domain
            var userProfileDomain = new UserProfile
            {
                DisplayName = restaurantProfileDto.DisplayName
            };

            var geocodeResponse = geocodeService.Geocode(restaurantProfileDto.Address);
            if (geocodeResponse.Error != null)
            {
                return new ResponseDto<bool>
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }


            // Extract restaurant profile domain
            var restaurantProfileDomain = new RestaurantProfile(
                restaurantProfileDto.PhoneNumber,
                restaurantProfileDto.Address,
                restaurantProfileDto.Details);

            restaurantProfileDomain.GeoCoordinates = new GeoCoordinates(geocodeResponse.Data.Latitude, geocodeResponse.Data.Longitude);

            // Extract business hours domains
            var businessHourDtos = restaurantProfileDto.BusinessHourDtos;


            // Extract restaurant menu dictionary
            var restaurantMenuDomains = restaurantProfileDto.RestaurantMenusList;

            // Execute update of database
            var profileGateway = new RestaurantProfileGateway();

            var responseDtoFromGateway = profileGateway.EditRestaurantProfileById(userAccountResponseDto.Data.Id, userProfileDomain, restaurantProfileDomain, businessHourDtos, restaurantMenuDomains);

            return responseDtoFromGateway;
        }

        //ImageUploadManager
        // TODO: @Angelica Add image profile upload here
        public ResponseDto<bool> MenuItemImageUpload(HttpPostedFile image, string username, int menuId)
        {
            var user = new UserProfileDto() { Username = username };

            var ImageUploadValidationStrategy = new ImageUploadValidationStrategy(user, image);
            var result = ImageUploadValidationStrategy.ExecuteStrategy();

            if (result.Data == false)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = result.Error
                };
            }

            // Store file extension from file
            var fileExtension = Path.GetExtension(image.FileName);

            // Image name = menuId + extension
            var newImagename = menuId + fileExtension;

            // Saving Virtual Path
            var virtualPath = ImagePaths.VIRTUAL_MENU_ITEM_PATH + newImagename;

            // Save physical Path
            string physicalPath = ImagePaths.PHYSICAL_MENU_ITEM_PATH + newImagename;
        
            // Call gateway to save path to database
            using (var gateway = new RestaurantProfileGateway())
            {
                var gatewayresult = gateway.UploadImage(user, virtualPath, menuId);
                if (gatewayresult.Data == false)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = gatewayresult.Error
                    };
                }

                image.SaveAs(physicalPath);

                return new ResponseDto<bool>
                {
                    Data = true
                };
            }
        }
    }
}