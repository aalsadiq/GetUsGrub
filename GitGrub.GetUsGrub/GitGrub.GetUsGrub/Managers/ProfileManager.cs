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
        /// Gets regular profile information by userId
        /// </summary>
        /// <param name="regularProfileDto">Receives RegularProfile object containing userId</param>
        /// <returns>Returns the regularProfile information</returns>
        public RegularProfile GetRegularProfile(RegularProfile regularProfileDto)
        {
            // call getregularprofile gateway
            return regularProfileDto;
        }

        /// <summary>
        /// Gets restaurant profile information by userId
        /// </summary>
        /// <param name="restaurantProfileDto">Receives RestaurantProfile object containing userId</param>
        /// <returns>Returns the restaurantProfile information</returns>
        public RestaurantProfile GetRestaurantProfile(RestaurantProfile restaurantProfileDto)
        {
            // call getrestaurantprofile gateway
            return restaurantProfileDto;
        }
        /// <summary>
        /// Applies changes to a regular profile
        /// </summary>
        /// <param name="regularProfile">Sending a RegularProfile object acts like a DTO</param>
        /// <returns>True if edit is successfully saved to the database</returns>
        public bool EditRegularProfile(RegularProfile regularProfileDto)
        {
            // call editregularprofile gateway

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restaurantProfile">Sending a RestaurantProfile object acts like a DTO</param>
        /// <returns>True if edit is succedssfully saved to the database</returns>
        public bool EditRestaurantProfile(RestaurantProfile restaurantProfileDto)
        {
            // call editrestaurantprofile gateway
            return true;
        }
    }
}