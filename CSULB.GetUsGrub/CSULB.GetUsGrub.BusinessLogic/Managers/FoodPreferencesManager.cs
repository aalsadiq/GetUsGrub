using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.Collections.Generic;

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
        public ResponseDto<ICollection<string>> GetFoodPreferences(string tokenString)
        {
            // Extract username from token string
            var tokenService = new TokenService();
            var username = tokenService.GetTokenUsername(tokenString);

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
                Error = GeneralErrorMessages.GENERAL_ERROR
            };
        }

        /// <summary>
        /// Method to edit user's food preferences
        /// </summary>
        /// <param name="foodPreferencesDto"></param>
        /// <returns>Response DTO with a boolean determining success of the transaction</returns>
        public ResponseDto<bool> EditFoodPreferences(string tokenString, FoodPreferencesDto foodPreferencesDto)
        {
            // Extract username from token string
            var tokenService = new TokenService();
            var username = tokenService.GetTokenUsername(tokenString);

            using (var gateway = new UserGateway())
            {
                // Get list of current food preferences from database
                var currentFoodPreferences = gateway.GetFoodPreferencesByUsername(username).Data.FoodPreferences;

                // Get list of updated food preferences from dto
                var updatedFoodPreferences = foodPreferencesDto.FoodPreferences;

                // Call method to create lists of food preferences to be added and to be removed
                var preferencesToBeAdded = LeftOuterJoin(updatedFoodPreferences, currentFoodPreferences);
                var preferencesToBeRemoved = LeftOuterJoin(currentFoodPreferences, updatedFoodPreferences);

                // Call gateway to update user's food preferences
                var result = gateway.EditFoodPreferencesByUsername(username, preferencesToBeAdded, preferencesToBeRemoved);

                // Return boolean determining success of update
                return result;
            }
        }
        
        /// <summary>
        /// Method to get the left outer join of list one with list two
        /// </summary>
        /// <param name="listOne"></param>
        /// <param name="listTwo"></param>
        /// <returns>Result of the left outer join of two lists</returns>
        private ICollection<string> LeftOuterJoin(ICollection<string> listOne, ICollection<string> listTwo)
        {
            // Create a list containing the left outer join result
            var result = new List<string>();

            // Iterate through items in list one
            foreach (var item in listOne)
            {
                // If item does not exist in list two, add to left outer join result
                if (!listTwo.Contains(item))
                {
                    result.Add(item);
                }
            }

            // Return the result
            return result;
        }
    }
} 
