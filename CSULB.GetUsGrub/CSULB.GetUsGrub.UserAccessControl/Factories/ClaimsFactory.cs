using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
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
                new Claim(ActionConstant.READ+ResourceConstant.INDIVIDUAL, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.INDIVIDUAL, "True"),

                // For Food Preferences
                new Claim(ActionConstant.READ+ResourceConstant.PREFERENCES, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.PREFERENCES, "True"),

                // For Bill Splitter
                new Claim(ActionConstant.READ+ResourceConstant.MENU, "True"),
                new Claim(ActionConstant.READ+ResourceConstant.DICTIONARY, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.DICTIONARY, "True")
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
                new Claim(ActionConstant.READ+ResourceConstant.RESTAURANT, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.RESTAURANT, "True"),

                // For Bill Splitter
                new Claim(ActionConstant.READ+ResourceConstant.MENU, "True")
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
                new Claim(ActionConstant.CREATE+ResourceConstant.USER, "True"),
                new Claim(ActionConstant.READ+ResourceConstant.USER, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.USER, "True"),
                new Claim(ActionConstant.DELETE+ResourceConstant.USER, "True"),
                new Claim(ActionConstant.DEACTIVATE+ResourceConstant.USER, "True"),
                new Claim(ActionConstant.REACTIVATE+ResourceConstant.USER, "True"),

                // For Indvidiual and Restaurant Profile Management
                new Claim(ActionConstant.READ+ResourceConstant.INDIVIDUAL, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.INDIVIDUAL, "True"),
                new Claim(ActionConstant.READ+ResourceConstant.RESTAURANT, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.RESTAURANT, "True"),

                // For Food Preferences
                new Claim(ActionConstant.CREATE+ResourceConstant.PREFERENCES, "True"),
                new Claim(ActionConstant.READ+ResourceConstant.PREFERENCES, "True"),
                new Claim(ActionConstant.UPDATE+ResourceConstant.PREFERENCES, "True"),
                new Claim(ActionConstant.DELETE+ResourceConstant.PREFERENCES, "True"),

                // For Bill Splitter
                new Claim(ActionConstant.READ+ResourceConstant.DICTIONARY, "True"),
                new Claim(ActionConstant.READ+ResourceConstant.MENU, "True"),
            };
        }
    }
}
