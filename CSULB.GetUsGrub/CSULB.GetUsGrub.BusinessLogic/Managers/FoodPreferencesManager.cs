using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Business logic for pages pertaining to food preferences
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/14/18
    /// </summary>
    public class FoodPreferencesManager
    {
        /// <summary>
        /// Method to get user's food preferences given the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Food Preferences DTO within the Response DTO</returns>
        public ResponseDto<FoodPreferencesDto> GetFoodPreferences(string username)
        {
            using (var gateway = new UserGateway())
            {
                // Call the user gateway to use the method to get food preferences by username
                var preferences = gateway.GetFoodPreferencesByUsername(username);

                // If no error occurs, return the DTO
                if (preferences.Error != null)
                {
                    return preferences;
                }
            }

            // Otherwise, return a DTO with error message
            return new ResponseDto<FoodPreferencesDto>
            {
                Error = "Something went wrong."
            };
        }
    }
}
