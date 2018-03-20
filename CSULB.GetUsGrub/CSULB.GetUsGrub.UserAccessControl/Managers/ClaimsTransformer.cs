using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using System;
using System.Linq;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Create a new ClaimsPrincipal with the user's permission claims
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 3/17/18
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
            // Get the username claim for the ClaimsPrincipal
            ClaimsPrincipal principal = incomingPrincipal;
            string username = principal.FindFirst("username").Value;

            // If username is null, throw exception
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new SecurityException("Username not found.");
            }

            // If resourceName is null, throw exception
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new SecurityException("Requested resource name is invalid.");
            }

            // If username is valid, but does not exist in the database

            // Create the new ClaimsPrincipal
            principal = CreatePrincipal(username, resourceName);

            // Return proper ClaimsPrincipal
            return principal;
        }

        /// <summary>
        /// Generate a new ClaimsPrincipal with all of the user's read or permission claims
        /// bassed on the resourceName that is passed in (read or permission)
        /// </summary>
        /// <param name="username"></param>
        /// <returns>New ClaimsPrincipal with</returns>
        private ClaimsPrincipal CreatePrincipal(string username, string resourceName)
        {
            // Get user's list of claims from databases
            // var gateway = new UserAccessGateway(); ???
            // claims = gateway.GetClaimsByUsername(username); ???
            using(var gateway = new AuthorizationGateway())
            {
                var claims = gateway.GetClaimsByUsername(username);
            }
            // For testing
            ClaimsFactory factory = new ClaimsFactory();
            List<Claim> claims = factory.CreateAdminClaims().ToList();

            // If claims do not exist
            if (claims == null)
            {
                throw new System.Exception("No claims for found for this user.");
            }

            // If resourceName is read, filter out the claims that are not read claims
            if (resourceName == "read")
            {
                claims.RemoveAll(x => !x.Type.StartsWith("Read"));
            }

            // Create ClaimsIdentity with the list of claims
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, resourceName);

            // Create ClaimsPrincipal with the ClaimsIdentity
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Return the new ClaimsPrincipal
            return claimsPrincipal;
        }
    }
}
