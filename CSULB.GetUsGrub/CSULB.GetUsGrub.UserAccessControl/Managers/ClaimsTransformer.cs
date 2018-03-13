using System.Collections.Generic;
using System.Security;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Create a new ClaimsPrincipal with the user's permission claims
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 3/13/18
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

            // Create a new principal
            ClaimsPrincipal principal = new ClaimsPrincipal();

            // If trying to get all permissions, create ClaimsPrincipal
            // with all permissions
            if (resourceName == "permissions")
            {
                principal = CreatePrincipal(username);
            }

            // If trying to get only read permissions, create ClaimsPrincipal
            // with only read permissions
            if (resourceName == "read")
            {
                principal = CreateReadPrincipal(username);
            }

            // Return proper ClaimsPrincipal
            return principal;
        }

        // TODO: @Ahmed How would you like to get claims?
        /// <summary>
        /// Generate a new ClaimsPrincipal with all of the user's permission claims
        /// </summary>
        /// <param name="username"></param>
        /// <returns>New ClaimsPrincipal with</returns>
        private ClaimsPrincipal CreatePrincipal(string username)
        {
            // Get user's list of claims from database
            // Placeholder; waiting for Data Access
            // var gateway = new UserAccessGateway; ???
            // claims = gateway.GetClaimsByUsername(username); ???
            ClaimsFactory factory = new ClaimsFactory();
            ICollection<Claim> claims = factory.CreateAdminClaims();

            // Create ClaimsIdentity with the list of claims
            var id = new ClaimsIdentity(claims, "permission");

            // Create ClaimsPrincipal with the ClaimsIdentity
            var claimsPrincipal = new ClaimsPrincipal(id);

            // Return the new ClaimsPrincipal
            return claimsPrincipal;
        }

        /// <summary>
        /// Generate a new ClaimsPrincipal containing only the user's read permission claims.
        /// Purpose is for authentication to handle access control between pages in the front-end.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>New ClaimsPrincipal with user's read claims</returns>
        private ClaimsPrincipal CreateReadPrincipal(string username)
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
