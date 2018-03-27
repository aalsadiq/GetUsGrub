using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Checks if a user has access whenever a request hits the Authorization Filters
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 03/14/18
    /// </summary>
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        /// <summary>
        /// Check if a user has access by running through their list of permission claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns>True or False on whether user has access</returns>
        public override bool CheckAccess(AuthorizationContext context)
        {
            // Create the claim based on the resource action pair
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;
            string claim = string.Concat(action, resource);

            // This is here to create a tester ClaimsPrincipal for testing purposes
            // Supposed to be var p = context.Principal; which is taken from the token
            var p = CreateTestPrincipal();

            // Pass principal with just the username claim into the ClaimsTransformer
            // to grab appropriate permission claims for the rest of the application
            ClaimsTransformer transformer = new ClaimsTransformer();
            ClaimsPrincipal principal = transformer.Authenticate("permissions", p);

            // Check if the ClaimsPrincipal contains the claim
            bool hasAccess = principal.HasClaim(claim, "True");

            // Return true or false
            return hasAccess;
        }

        /// <summary>
        /// Create a test ClaimsPrincipal to test the User Controller. Contains only the name claim.
        /// To be passed into the ClaimsTransformer to grab all admin claims based on the name.
        /// 
        /// </summary>
        /// <returns>ClaimsPrincipal for testing</returns>
        private ClaimsPrincipal CreateTestPrincipal()
        {
            // Test Principal with "Admin" username to test the User Controller
            var testClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Admin")
            };

            // Create Test ClaimsIdentity
            ClaimsIdentity testClaimsIdentity = new ClaimsIdentity(testClaims, "Test");

            // Create Test ClaimsPrincipal
            ClaimsPrincipal testClaimsPrincipal = new ClaimsPrincipal(testClaimsIdentity);

            // Return Test Claimsprincipl
            return testClaimsPrincipal;
        }
    }
}
