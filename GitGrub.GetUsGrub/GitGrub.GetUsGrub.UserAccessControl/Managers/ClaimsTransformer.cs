using System.Collections.Generic;
using System.Security;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Create a new ClaimsPrincipal with the user's claims
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 3/08/18
    /// </summary>
    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        /// <summary>
        /// Makes sure user authenticated is valid before generating new ClaimsPrincipal
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="incomingPrincipal"></param>
        /// <returns>The new ClaimsPrincipal</returns>
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            var username = incomingPrincipal.Identity.Name;

            // If username is not valid, throw exception
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new SecurityException("Username not found.");
            }

            // If username is valid, call method to create the ClaimsPrincipal
            return CreatePrincipal(username);
        }

        /// <summary>
        /// Generate ClaimsIdentity and ClaimsPrincipal given the Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>The new ClaimsPrincipal</returns>
        public ClaimsPrincipal CreatePrincipal(string username)
        {
            // Get user's list of claims from database
            // Placeholder; waiting for Data Access
            // var gateway = new UserAccessGateway; ???
            // claims = gateway.GetClaimsByUsername(username); ???
            var claims = new List<Claim>();

            // Create ClaimsIdentity with the list of claims
            var id = new ClaimsIdentity(claims, "custom");

            // Create ClaimsPrincipal with the ClaimsIdentity
            var claimsPrincipal = new ClaimsPrincipal(id);

            // Return the new ClaimsPrincipal
            return claimsPrincipal;
        }

    }
}