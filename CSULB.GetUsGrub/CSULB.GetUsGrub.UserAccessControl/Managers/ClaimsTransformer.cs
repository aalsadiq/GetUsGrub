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
    /// @updated: 04/24/18
    /// </summary>
    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        /// <summary>
        /// Authenticate and transform the given claims principal
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="incomingPrincipal"></param>
        /// <returns>The new ClaimsPrincipal</returns>
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            // Get the username claim for the claims principal
            string username = incomingPrincipal.FindFirst(ResourceConstant.USERNAME).Value;

            // Retrieve user's claims if user is valid, otherwise a security exception is thrown
            var claims = GetUserClaims(username);

            // Otherwise, call method to create a claims principal with permission claims
            return CreateClaimsPrincipal(claims);
        }

        /// <summary>
        /// Method using the gateway to retrieve user's permission claims given the username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Collection of user's permission claims</returns>
        private ICollection<Claim> GetUserClaims(string username)
        {      
            using (var gateway = new AuthorizationGateway())
            {
                // Grab user's claims from the database
                var dbClaims = gateway.GetClaimsByUsername(username);

                // If an error occurs and user has no claims, check if user is valid
                if (dbClaims.Error != null)
                {
                    throw new SecurityException(GeneralErrorMessages.GENERAL_ERROR);
                }

                return dbClaims.Data;
            }
        }   
        
        /// <summary>
        /// Method to create a claims principal given a collection of claims
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
        /// Method to create a claims principal with claims pertaining to first time users from the SSO
        /// </summary>
        /// <returns></returns>
        public ClaimsIdentity CreateSsoClaimsIdentity(string username)
        {
            // Create claims pertaining to first time users
            var factory = new ClaimsFactory();
            var claims = factory.Create(AccountTypes.FirstTimeUser);
            claims.Add(new Claim(ResourceConstant.USERNAME, username));

            // Call method to create and return the new claims principal
            return new ClaimsIdentity(claims);
        }     
        
        /// <summary>
        /// Method to create a claims identity with only the username and "Read" claims
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Claims identity with username and "Read" claims</returns>
        public ClaimsIdentity CreateAuthenticationClaimsIdentity(string username)
        {
            // Call method to get user's claims
            var claims = GetUserClaims(username).ToList();

            // Remove all claims that are not read claims
            claims.RemoveAll(x => !x.Type.StartsWith(ActionConstant.READ));

            // Add username claim
            claims.Add(new Claim(ResourceConstant.USERNAME, username));

            // Call method to create and return the new claims principal
            return new ClaimsIdentity(claims);
        }
    }
}
