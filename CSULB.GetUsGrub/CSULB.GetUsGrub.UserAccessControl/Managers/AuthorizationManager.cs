using System;
using System.Linq;
using System.Security;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Determines whether a user has access to a particular resource by checking their claims.
    /// @author: Rachel Dang
    /// @updated: 04/24/18
    /// </summary>
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        /// <summary>
        /// Check if a user has access to a particular resource given the authorization context
        /// </summary>
        /// <param name="context"></param>
        /// <returns>True or False on whether user has access</returns>
        public override bool CheckAccess(AuthorizationContext context)
        {
            // Extract the data from the authorization context
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;
            var principal = context.Principal;

            // Create the claim needed to access the resource
            string claim = string.Concat(action, resource);

            try
            {
                // Transform the claims principal to contain all of the user's permission claims
                ClaimsTransformer transformer = new ClaimsTransformer();
                principal = transformer.Authenticate(PermissionTypes.Authorization, principal);

                // Check transformed claims principal to see if it contains the claim needed
                bool hasAccess = principal.HasClaim(claim, "True");

                // Return true or false determining user access
                return hasAccess;
            }          
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                // If an error occurs, throw a security exception
                throw new SecurityException();
            }
           
        }
    }
}
