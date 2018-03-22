using System.Linq;
using System.Security;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Determines whether a user has access to a particular resource by checking their claims.
    /// @author: Rachel Dang
    /// @updated: 03/22/18
    /// </summary>
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        /// <summary>
        /// Check if a user has access to a particular resource given the authorization context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>True or False on whether user has access</returns>
        public override bool CheckAccess(AuthorizationContext context)
        {
            // Create the claim needed to access the resource
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;
            string claim = string.Concat(action, resource);

            // Create a new principal with the read-only principal give from context
            ClaimsPrincipal principal = context.Principal;

            // Get the username claim from the principal
            var username = principal.FindFirst("username").Value;

            // Check if username is valid
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new SecurityException("Username is invalid.");
            }

            // Pass principal into transformer to create principal with all of the user's claims
            ClaimsTransformer transformer = new ClaimsTransformer();
            principal = transformer.Authenticate("permission", principal);

            // Check principal to see if it contains the claim needed
            bool hasAccess = principal.HasClaim(claim, "True");

            // Return true or false
            return hasAccess;
        }
    }
}
