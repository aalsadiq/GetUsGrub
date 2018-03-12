using System.Collections.Generic;
using System.Security;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Create a new ClaimsPrincipal with the user's permission claims
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

        // TODO: @Ahmed How would you like to get claims?
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
            var id = new ClaimsIdentity(claims, "permission");

            // Create ClaimsPrincipal with the ClaimsIdentity
            var claimsPrincipal = new ClaimsPrincipal(id);

            // Return the new ClaimsPrincipal
            return claimsPrincipal;
        }

        /// <summary>
        /// Generate a new ClaimsPrincipal containing just the user's read claims.
        /// Purpose is for authentication to handle access control between pages in the front-end.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>New ClaimsPrincipal with user's read claims</returns>
        public ClaimsPrincipal GetReadClaims(string username)
        {
            // Get user's list of claims from database
            // Placeholder; waiting for Data Access
            // var gateway = new UserAccessGateway; ???
            // claims = gateway.GetReadClaimsByUsername(username); ???
            var claims = new List<Claim>();

            // Create ClaimsIdentity with the list of claims
            var id = new ClaimsIdentity(claims, "read");

            // Create ClaimsPrincipal with the ClaimsIdentity
            var claimsPrincipal = new ClaimsPrincipal(id);

            // Return the new ClaimsPrincipal
            return claimsPrincipal;
        }
    }
}
