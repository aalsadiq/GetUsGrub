using System.Collections.Generic;
using System.Security;
using System.Security.Claims;
using System.Linq;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

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
        /// Makes sure user authenticated is valid before generating new claims principal
        /// </summary>
        /// <param name="permissionType"></param>
        /// <param name="incomingPrincipal"></param>
        /// <returns>The new ClaimsPrincipal</returns>
        public override ClaimsPrincipal Authenticate(string permissionType, ClaimsPrincipal incomingPrincipal)
        {
            // Get the username claim for the claims principal
            string username = incomingPrincipal.FindFirst("Username").Value;

            // Authenticate that user is valid and grab user's claims from the database
            ICollection<Claim> claims;
            using (var gateway = new AuthorizationGateway())
            {
                var dbClaims = gateway.GetClaimsByUsername(username);

                // If an error occurs and user fails authentication, throw a security exception 
                if (dbClaims.Error != null)
                {
                    throw new SecurityException(GeneralErrorMessages.GENERAL_ERROR);
                }

                claims = dbClaims.Data;
            }

            // If user's claims are null, user is a first time user registered through the SSO
            if (!claims.Any())
            {
                return CreateSsoClaimsPrincipal();
            }

            // Create the new claims principal based on permission type
            switch (permissionType)
            {
                case PermissionTypes.All:
                    return CreateClaimsPrincipal(claims);
                case PermissionTypes.Read:
                    return CreateReadPermissionsClaimsPrincipal(username, claims);
                default:
                    return incomingPrincipal;
            }
        }

        /// <summary>
        /// Method to create a claims principal given the collection of claims
        /// </summary>
        /// <param name="claims"></param>
        /// <returns>Claims principal</returns>
        private ClaimsPrincipal CreateClaimsPrincipal(ICollection<Claim> claims)
        {
            // Create ClaimsIdentity with the list of claims
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

            // Create ClaimsPrincipal with the ClaimsIdentity
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Return the new claims principal
            return claimsPrincipal;
        }

        /// <summary>
        /// Method to create a claims principal with only the username and read claims
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="incomingPrincipal"></param>
        /// <returns>Claims principal with permissions from the database</returns>
        private ClaimsPrincipal CreateReadPermissionsClaimsPrincipal(string username, ICollection<Claim> claims)
        {
            // Convert the collection to a list
            var readClaims = claims.ToList();

            // Remove all claims that are not read claims
            readClaims.RemoveAll(x => !x.Type.StartsWith(PermissionTypes.Read));

            // Add username claim
            readClaims.Add(new Claim(ResourceConstant.USERNAME, username));

            // Call method to create and return the new claims principal
            return CreateClaimsPrincipal(readClaims);
        }

        /// <summary>
        /// Method to create a claims principal with claims pertaining to first time users through the SSO
        /// </summary>
        /// <returns></returns>
        private ClaimsPrincipal CreateSsoClaimsPrincipal()
        {
            // Create a new instance of the claims factory
            var factory = new ClaimsFactory();

            // Create claims pertaining to first time users
            var claims = factory.Create(AccountType.FirstTimeUser);

            // Call method to create and return the new claims principal
            return CreateClaimsPrincipal(claims);
        }
    }
}
