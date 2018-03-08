using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Factory that creates a set of claims for new users
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 3/08/18
    /// </summary>
    public class ClaimsFactory
    {
        /// <summary>
        /// Creates the claims for an Individual User
        /// </summary>
        /// <returns>Set of claims associated with an Individual Account</returns>
        public ICollection<Claim> CreateIndividualClaims()
        {
            return new List<Claim>
            {
                // For Individual Profie Management
                new Claim("ReadIndividualProfile", "True"),
                new Claim("UpdateIndividualProfile", "True"),

                // For Food Preferences
                new Claim("ReadPreferences", "True"),
                new Claim("UpdatePreferences", "True"),

                // For Bill Splitter
                new Claim("ReadBillSplitter", "True"),
                new Claim("ReadMenu", "True"),
                new Claim("ReadDictionary", "True"),
                new Claim("UpdateDictionary", "True")
            };
        }

        /// <summary>
        /// Creates the claims for a Restaurant User
        /// </summary>
        /// <returns>Set of claims associated with a Restaurant Account</returns>
        public ICollection<Claim> CreateRestaurantClaims()
        {
            return new List<Claim>
            {
                // For Restaurant Profie Management
                new Claim("ReadRestsaurantProfile", "True"),
                new Claim("UpdateRestaurantProfile", "True"),

                // For Bill Splitter
                new Claim("ReadBillSplitter", "True"),
                new Claim("ReadMenu", "True")
            };
        }

        /// <summary>
        /// Creates the claims for an Administrative User
        /// </summary>
        /// <returns>Set of claims associated with an Administrative Account</returns>
        public ICollection<Claim> CreateAdminClaims()
        {
            return new List<Claim>
            {
                // For User Management
                new Claim("CreateUser", "True"),
                new Claim("ReadUser", "True"),
                new Claim("UpdateUser", "True"),
                new Claim("DeleteUser", "True"),
                new Claim("DeactivateUser", "True"),
                new Claim("ReactivateUser", "True"),


                // For Indvidiual and Restaurant Profile Management
                new Claim("ReadIndividualProfile", "True"),
                new Claim("UpdateIndividualProfile", "True"),
                new Claim("ReadRestsaurantProfile", "True"),
                new Claim("UpdateRestaurantProfile", "True"),

                // For Food Preferences
                new Claim("CreatePreferences", "True"),
                new Claim("ReadPreferences", "True"),
                new Claim("DeletePreferences", "True"),

                // For Bill Splitter
                new Claim("ReadBillSplitter", "True"),
                new Claim("ReadMenu", "True"),
                new Claim("ReadDictionary", "True")
            };
        }
    }
}