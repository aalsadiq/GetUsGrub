using System;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for editing profile information
    /// </summary>
    public class ProfileManager
    {
        /// <summary>
        /// Applies changes to a regular profile
        /// </summary>
        /// <param name="regularProfile">Sending a RegularProfile object acts like a DTO</param>
        /// <returns>True if edit is successfully saved to the database</returns>
        public bool EditRegularProfile(RegularProfile regularProfileDto)
        {
            // call edit user gateway

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restaurantProfile">Sending a RestaurantProfile object acts like a DTO</param>
        /// <returns>True if edit is succedssfully saved to the database</returns>
        public bool EditRestaurantProfile(RestaurantProfile restaurantProfileDto)
        {
            // call edit user gateway
            return true;
        }
    }
}