using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Checks if a user has access whenever a request hits the Authorization Filters
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 03/18/18
    /// </summary>
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        /// <summary>
        /// Check if a user has access by their claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns>True or False on whether user has access</returns>
        public override bool CheckAccess(AuthorizationContext context)
        {
            // Create the claim the user needs given the resource and action
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;
            string claim = string.Concat(action, resource);

            // Takes in the Thread.CurrentPrincipal which only contains the username claim
            ClaimsPrincipal principal = context.Principal;

            // Pass ClaimsPrincipal with just the username claim into the ClaimsTransformer
            // which will return a new ClaimsPrincipal with the permission claims
            ClaimsTransformer transformer = new ClaimsTransformer();
            principal = transformer.Authenticate("permission", principal);

            // Check if the ClaimsPrincipal contains the claim
            bool hasAccess = principal.HasClaim(claim, "True");

            // Return true or false
            return hasAccess;
        }
    }
}
