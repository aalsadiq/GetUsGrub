using System;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Managers
{
    /// <summary>
    /// Manager responsible for editing profile information
    /// </summary>
    public class ProfileManager
    {
        /// <summary>
        /// Queries the db for the profile information of the regular user given by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>regularProfileDto containing regular profile information</returns>
        public RegularProfileDto GetRegularProfile(string username)
        {
            // call GetRegularProfileByUsername(string username) in gateway
            return regularProfileDto;
        }

        /// <summary>
        /// Queries the db for the profile information of the restaurant user given by the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>restaurantProfileDto containing restaurant profile information</returns>
        public RestaurantProfileDto GetRestaurantProfile(string username)
        {
            // call GetRestaurantProfileByUsername(string username) in gateway
            return restaurantProfileDto;
        }
        
        /// <summary>
        /// Edits the profile information of the regular user given by the username inside the editRegularProfileDto
        /// </summary>
        /// <param name="editRegularProfileDto">Contains the username along with the profile information the user wishes to update</param>
        /// <returns>Returns true if changes were successfully applied to the database</returns>
        public bool EditRegularProfile(EditRegularProfileDto editRegularProfileDto)
        {
            // call EditRegularProfileByUsername(EditRegularProfileDto editRegularProfileDto) in gateway

            return true;
        }

        /// <summary>
        /// Edits the profile information of the restaurant user given by the username inside the editRestaurantProfileDto
        /// </summary>
        /// <param name="editRestaurantProfileDto">Contains the username along with the profile information the user wishes to update</param>
        /// <returns>Returns true if changes were successfully applied to the database</returns>
        public bool EditRestaurantProfile(EditRestaurantProfileDto editRestaurantProfileDto)
        {
            // call EditRestaurantProfileByUsername(EditRestaurantProfileDto editRestaurantProfileDto) in gateway

            return true;
        }
    }
}