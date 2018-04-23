using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Business logic for pages pertaining to food preferences
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/17/18
    /// </summary>
    public class FoodPreferencesManager
    {
        /// <summary>
        /// Method to get user's food preferences given the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Food Preferences DTO within the Response DTO</returns>
        public ResponseDto<ICollection<string>> GetFoodPreferences(string username)
        {
            using (var gateway = new UserGateway())
            {
                // Call the user gateway to use the method to get food preferences by username
                var responseDto = gateway.GetFoodPreferencesByUsername(username);

                // If no error occurs, return the DTO
                if (responseDto.Error == null)
                {
                    return new ResponseDto<ICollection<string>>
                    {
                        Data = responseDto.Data.FoodPreferences
                    };
                }
            }

            // Otherwise, return a DTO with error message
            return new ResponseDto<ICollection<string>>
            {
                Error = "Something went wrong."
            };
        }

        /// <summary>
        /// Method to edit user's food preferences
        /// </summary>
        /// <param name="foodPreferencesDto"></param>
        /// <returns></returns>
        public ResponseDto<bool> EditFoodPreferences(FoodPreferencesDto foodPreferencesDto)
        {
            var updatedFoodPreferences = foodPreferencesDto.FoodPreferences;

            return new ResponseDto<bool>
            {
                Data = false,
                Error =  "Something went wrong; Edit Food Preferences."
            };
        }
    }
}
