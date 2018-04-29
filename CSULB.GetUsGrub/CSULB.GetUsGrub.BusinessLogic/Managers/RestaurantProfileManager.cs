using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Configuration;
using System.IO;
using System.Web;
using System.Linq;

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
            var tokenService = new TokenService();
            var restaurantBusinessHourDtoService = new RestaurantBusinessHourDtoService();

            // Retrieve account by username
            var userGateway = new UserGateway();

            // Call the gateway
            var userAccountResponseDto = userGateway.GetUserByUsername(tokenService.GetTokenUsername(token));

            // Retrieve restaurant profile from database
            var restaurantProfileGateway = new RestaurantProfileGateway();

            var restaurantProfileResponseDto = restaurantProfileGateway.GetRestaurantProfileById(userAccountResponseDto.Data.Id);

            // Call the RestaurantBusinessHourDtoService
            var convertedRestaurantBusinessHourDtos = restaurantBusinessHourDtoService.SetStringTimesFromDateTimes(restaurantProfileResponseDto.Data.BusinessHours);

            // Replace the BusinessHourDtos with the converted ones
            restaurantProfileResponseDto.Data.BusinessHours = convertedRestaurantBusinessHourDtos;

            return restaurantProfileResponseDto;
        }

        public ResponseDto<bool> EditProfile(RestaurantProfileDto restaurantProfileDto, string token)
        {
            var geocodeService = new GoogleGeocodeService();
            var restaurantBusinessHourDtoService = new RestaurantBusinessHourDtoService();
            var tokenService = new TokenService();

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
                restaurantProfileDto.Details)
            {
                GeoCoordinates = new GeoCoordinates(geocodeResponse.Data.Latitude, geocodeResponse.Data.Longitude)
            };

            // Extract business hours domains
            var restaurantBusinessHourDtos = restaurantProfileDto.BusinessHours;

            // Call the RestaurantBusinessHourDtoService
            var convertedRestaurantBusinessHourDtos = restaurantBusinessHourDtoService.SetDateTimesFromStringTimes(restaurantBusinessHourDtos);

            // Extract restaurant menus
            if (restaurantProfileDto.RestaurantMenusList.Count == 0)
            {
                
            }
            var restaurantMenuDomains = restaurantProfileDto.RestaurantMenusList;

            // Execute update of database
            var profileGateway = new RestaurantProfileGateway();

            var responseDtoFromGateway = profileGateway.EditRestaurantProfileById(userAccountResponseDto.Data.Id, userProfileDomain, restaurantProfileDomain, convertedRestaurantBusinessHourDtos, restaurantMenuDomains);

            return responseDtoFromGateway;
        }

        /// <summary>
        /// Uploads a menu image for the specified username and menu id.
        /// <para>
        /// @author: Angelica Salas Tovar
        /// @update: 04/26/2018
        /// </para>
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="username">The user</param>
        /// <param name="menuId">The menu id</param>
        /// <returns></returns>
        public ResponseDto<bool> MenuItemImageUpload(HttpPostedFile image, string username, int menuId)
        {
            var user = new UserProfileDto() { Username = username };
            
            // Image Validations
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

            // Setting new image name
            var fileExtension = Path.GetExtension(image.FileName);
            var newImagename = username + fileExtension;

            // Mapping Image
            var urlPath = ConfigurationManager.AppSettings["URLMenuItemPath"];
            var url = urlPath + newImagename;

            // Setting image to user
            user.DisplayPicture = url;

            // Physical image path
            var physicalPath = ConfigurationManager.AppSettings["PhysicalMenuItemPath"];
            var physicalProfileImagePath = physicalPath + newImagename;

            // Call gateway to save virtualPath to database
            using (var gateway = new RestaurantProfileGateway())
            {
                var gatewayresult = gateway.UploadImage(user, url, menuId);
                if (gatewayresult.Data == false)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = gatewayresult.Error
                    };
                }

                // Save the image to the physical path
                image.SaveAs(physicalPath);

                return new ResponseDto<bool>
                {
                    Data = true
                };
            }
        }
    }
}