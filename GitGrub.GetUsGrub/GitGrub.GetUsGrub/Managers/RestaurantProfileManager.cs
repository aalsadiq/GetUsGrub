using System;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for retrieving and editing restaurant profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/21/18
    /// </summary>
    public class RestaurantProfileManager
    {
        private readonly RestaurantGateway _restaurantGateway;

        // need to update with brian gateway
        public RestaurantProfileManager(RestaurantGateway restaurantGateway)
        {
            _restaurantGateway = restaurantGateway;
        }

        /// <summary>
        /// Get restaurant profile by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>ResponseDto containing RestaurantDto containing user's restaurant profile information</returns>
        public ResponseDto<RestaurantProfileDto> GetRestaurantProfile(string username)
        {
            // call GetRestaurantProfileByUsername(string username) in gateway
            RestaurantProfileDto profile = _restaurantGateway.GetRestaurantProfileByUsername(username);

            try
            {
                ResponseDto<RestaurantProfileDto> response = new ResponseDto<RestaurantProfileDto>
                {
                    Data = profile
                };
                return response;
            }

            catch (Exception e)
            {
                ResponseDto<RestaurantProfileDto> response = new ResponseDto<RestaurantProfileDto>
                {
                    Error = e
                };
                return response;
            }
        }

        /// <summary>
        /// Edit restaurant profile by username
        /// </summary>
        /// <param name="editRestaurantProfileDto">Contains username and new restaurant profile information</param>
        /// <returns>ResponseDto containing RestaurantProfileDto containing user's restaurant profile information</returns>
        public ResponseDto<bool> EditRestaurantProfile(EditRestaurantProfileDto editRestaurantProfileDto)
        {
            // call EditRestaurantProfileByUsername(EditRestaurantProfileDto editRestaurantProfileDto) in gateway
            bool isEditSuccessful = _restaurantGateway.EditRestaurantProfileByUsername(editRestaurantProfileDto);

            try
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Data = isEditSuccessful
                };
                return response;
            }

            catch (Exception e)
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Error = e
                };
                return response;
            }
        }
    }
}