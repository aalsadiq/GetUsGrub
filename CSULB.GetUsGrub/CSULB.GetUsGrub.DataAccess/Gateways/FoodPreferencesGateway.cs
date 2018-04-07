
using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.DataAccess.Gateways
{
    class FoodPreferencesGateway
    {
        /// <summary>
        /// Method to retrieve user's claims from the database given the username.
        /// @author: Rachel Dang
        /// @updated: 04/07/18
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User's collection of food preferences</returns>
        public ResponseDto<FoodPreferences> GetFoodPreferencesByUsername(string username)
        {
            // Find the user account associated with the username
            using (var context = new FoodPreferencesContext())
            {
                try
                {
                    // Get the Food Preference object from the database
                    var preferences = (from foodPreferences in context.FoodPreferences
                                        where foodPreferences.UserAccount.Username == username
                                        select foodPreferences).FirstOrDefault();

                    // If claims are found, return the claims from the database
                    return new ResponseDto<FoodPreferences>
                    {
                        Data = preferences
                    };
                }
                

                // If no Food Preference is found, return an error
                catch (Exception exception)
                {
                    return new ResponseDto<FoodPreferences>
                    {
                        Error = exception.Message
                    };
                }
            }
        }
    }
}
