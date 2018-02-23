using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Factory that creates a set of claims for new users
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 2/21/18
    /// </summary>
    class ClaimsFactory
    {
        /// <summary>
        /// Creates the claims for an Individual User
        /// </summary>
        /// <returns>Set of claims associated with an Individual Account</returns>
        private IEnumerable<Claim> CreateIndividualClaims()
        {
            return new List<Claim>
            {
                // For Profie Management
                new Claim(ResourceConstant.RESOURCETYPE_PROFILE, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_PROFILE, ActionConstant.ACTIONTYPE_UPDATE),

                // For Food Preferences
                new Claim(ResourceConstant.RESOURCETYPE_PROFILE, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_PREFERENCES, ActionConstant.ACTIONTYPE_UPDATE),

                // For Bill Splitter
                new Claim(ResourceConstant.RESOURCETYPE_MENU, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_DICTIONARY, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_DICTIONARY, ActionConstant.ACTIONTYPE_UPDATE)
            };
        }

        /// <summary>
        /// Creates the claims for a Restaurant User
        /// </summary>
        /// <returns>Set of claims associated with a Restaurant Account</returns>
        public IEnumerable<Claim> CreateRestaurantClaims()
        {
            return new List<Claim>
            {
                // For Profile Mangement
                new Claim(ResourceConstant.RESOURCETYPE_RESTAURANTPROFILE, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_RESTAURANTPROFILE, ActionConstant.ACTIONTYPE_UPDATE),

                // For Bill Splitter
                new Claim(ResourceConstant.RESOURCETYPE_MENU, ActionConstant.ACTIONTYPE_READ)
            };
        }

        /// <summary>
        /// Creates the claims for an Administrative User
        /// </summary>
        /// <returns>Set of claims associated with an Administrative Account</returns>
        public IEnumerable<Claim> CreateAdminClaims()
        {
            return new List<Claim>
            {
                // For User Management
                new Claim(ResourceConstant.RESOURCETYPE_USER, ActionConstant.ACTIONTYPE_CREATE),
                new Claim(ResourceConstant.RESOURCETYPE_USER, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_USER, ActionConstant.ACTIONTYPE_UPDATE),
                new Claim(ResourceConstant.RESOURCETYPE_USER, ActionConstant.ACTIONTYPE_DELETE),
                new Claim(ResourceConstant.RESOURCETYPE_USER, ActionConstant.ACTIONTYPE_DEACTIVATE),
                new Claim(ResourceConstant.RESOURCETYPE_USER, ActionConstant.ACTIONTYPE_REACTIVATE),

                // For Indvidiual and Restaurant Profile Management
                new Claim(ResourceConstant.RESOURCETYPE_PROFILE, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_PROFILE, ActionConstant.ACTIONTYPE_UPDATE),
                new Claim(ResourceConstant.RESOURCETYPE_RESTAURANTPROFILE, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_RESTAURANTPROFILE, ActionConstant.ACTIONTYPE_UPDATE),

                // For Food Preferences
                new Claim(ResourceConstant.RESOURCETYPE_PREFERENCES, ActionConstant.ACTIONTYPE_CREATE),
                new Claim(ResourceConstant.RESOURCETYPE_PREFERENCES, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_PREFERENCES, ActionConstant.ACTIONTYPE_DELETE),

                // For Bill Splitter
                new Claim(ResourceConstant.RESOURCETYPE_MENU, ActionConstant.ACTIONTYPE_READ),
                new Claim(ResourceConstant.RESOURCETYPE_DICTIONARY, ActionConstant.ACTIONTYPE_READ)
            };
        }
    }
}