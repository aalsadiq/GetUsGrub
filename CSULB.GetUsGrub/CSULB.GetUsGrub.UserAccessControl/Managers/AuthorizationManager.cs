using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.UserAccessControl
{
    /// <summary>
    /// Checks if a user has access whenever a request hits the Authorization Filters
    /// 
    /// Author: Rachel Dang
    /// Last Updated: 03/08/18
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
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;

            // This is here for testing purposes
            var p = CreateTestPrincipal();

            if (resource == ResourceConstant.RESOURCETYPE_USER && action == ActionConstant.ACTIONTYPE_CREATE)
            {
                bool hasAccess = context.Principal.HasClaim("CreateUser", "True");
                return hasAccess;
            }

            return false;
        }

        /// <summary>
        /// This is for me to run testing.
        /// Create a tester ClaimsPrincipal holding a tester ClaimsIdentity which holds
        /// a list of tester Claims to make sure my attributes run correctly.
        /// </summary>
        /// <returns></returns>
        private ClaimsPrincipal CreateTestPrincipal()
        {
            var testClaims = new List<Claim>
            {
                new Claim("CreateUser", "True")
            };

            ClaimsIdentity testClaimsIdentity = new ClaimsIdentity(testClaims, "Test");
            ClaimsPrincipal testClaimsPrincipal = new ClaimsPrincipal(testClaimsIdentity);

            return testClaimsPrincipal;
        }
    }
}
