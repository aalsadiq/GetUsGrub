using GitGrub.GetUsGrub.Helpers;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for retrieving and editing restaurant profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/22/18
    /// </summary>
    public class RestaurantProfileManager
    {
        /// <summary>
        /// Retrieves restaurant profile information at username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Restaurant profile information encapsulatedin ResponseDto<RestaurantProfileDto></returns>
        public ResponseDto<RestaurantProfileDto> GetRestaurantProfile(string username)
        {
            try
            {
                using (var gateway = new RestaurantProfileGateway())
                {
                    RestaurantProfileDto profile = gateway.GetRestaurantProfileByUsername(username);
                    ResponseDto<RestaurantProfileDto> response = new ResponseDto<RestaurantProfileDto>
                    {
                        Data = profile
                    };
                    return response;
                }
            }

            catch
            {
                ResponseDto<RestaurantProfileDto> response = new ResponseDto<RestaurantProfileDto>
                {
                    Error = new CustomException("GetRestaurantProfile - Something went wrong.")
                };
                return response;
            }
        }

        /// <summary>
        /// Edits restaurant profile information at username
        /// </summary>
        /// <param name="editRestaurantProfileDto">Contains username and restaurant profile information</param>
        /// <returns>Returns true if edit is successful</returns>
        public ResponseDto<bool> EditRestaurantProfile(EditRestaurantProfileDto editRestaurantProfileDto)
        {
            try
            {
                using (var gateway = new RestaurantProfileGateway())
                {
                    bool isEditSuccessful = gateway.EditRestaurantProfileByUsername(editRestaurantProfileDto);

                    if (isEditSuccessful == true)
                    {
                        ResponseDto<bool> response = new ResponseDto<bool>
                        {
                            Data = isEditSuccessful
                        };
                        return response;
                    }

                    else
                    {
                        ResponseDto<bool> response = new ResponseDto<bool>
                        {
                            Error = new CustomException("Restaurant profile failed to update.")
                        };
                        return response;
                    }
                }
            }

            catch
            {
                ResponseDto<bool> response = new ResponseDto<bool>
                {
                    Error = new CustomException("EditRestaurantProfile - Something went wrong.")
                };
                return response;
            }
        }
    }
}