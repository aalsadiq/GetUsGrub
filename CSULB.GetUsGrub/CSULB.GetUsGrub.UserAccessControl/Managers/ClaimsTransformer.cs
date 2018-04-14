﻿using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using System.Linq;
using CSULB.GetUsGrub.DataAccess;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Create a new ClaimsPrincipal with the user's permission claims
    /// 
    /// @author: Rachel Dang
    /// @updated: 3/21/18
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
            string username = principal.FindFirst("Username").Value;

            // Create the new ClaimsPrincipal
            principal = CreatePrincipal(resourceName, principal);

            // Return proper ClaimsPrincipal
            return principal;
        }

        /// <summary>
        /// Generate a new claims principal containing all of the user's permission claims
        /// or just the read claims based on the resource name ("read", "permission").
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="incomingPrincipal"></param>
        /// <returns>Claims principal with permissions from the database</returns>
        private ClaimsPrincipal CreatePrincipal(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            // Take the claims from the incoming principal
            var username = incomingPrincipal.FindFirst("Username").Value;
            List<Claim> claims = new List<Claim> { };

            // Add to list with user's list of claims from database
            using(var gateway = new AuthorizationGateway())
            {
                // Grab user's list of claims from the database
                var userClaims = gateway.GetClaimsByUsername(username);
                
                // TODO @Rachel SSO user claims will be null so we have to revisit this [-Ahmed]
                // If user is invalid and an error returns
                if (userClaims.Error != null)
                {
                    throw new SecurityException(userClaims.Error);
                }

                // If user is valid, proceed to add claims
                foreach (var claim in userClaims.Data)
                {
                    claims.Add(claim);
                }
            }

            // If resourceName is read, filter out the claims that are not read claims
            if (resourceName == "read")
            {
                claims.RemoveAll(x => !x.Type.StartsWith("Read"));
            }

            // Add original username claim
            claims.Add(new Claim("Username", username));

            // Create ClaimsIdentity with the list of claims
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, resourceName);

            // Create ClaimsPrincipal with the ClaimsIdentity
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Return the new ClaimsPrincipal
            return claimsPrincipal;
        }
    }
}
