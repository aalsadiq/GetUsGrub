using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading;
using System.Web.Http;
using System.Linq;
using CSULB.GetUsGrub.UserAccessControl;

namespace CSULB.GetUsGrub
{
    /// <summary>
    /// Test controller used to test authorization filters.
    /// @author: Rachel Dang
    /// @updated: 03/22/2018
    /// </summary>
    public class UserAccessTestController : ApiController
    {
        /// <summary>
        /// Create mock data in place of the authentication filter.
        /// </summary>
        public UserAccessTestController()
        {
            // Create ClaimsFactory to create admin claims
            var factory = new ClaimsFactory();
            List<Claim> claims = factory.CreateIndividualClaims().ToList();

            // Add "username" claim
            claims.Add(new Claim("username", "Individual"));

            // Create claims principal
            ClaimsIdentity identity = new ClaimsIdentity(claims);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            //// Test for only read claims; requested only by authentication
            //ClaimsTransformer transformer = new ClaimsTransformer();
            //principal = transformer.Authenticate("read", principal);

            // Set Thread.CurrentPrincipal so the filters can grab the claims principal
            Thread.CurrentPrincipal = principal;
        }

        /// <summary>
        /// Access for be allowed for everyone. Pass.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/UAC")]
        public IHttpActionResult GetAnon()
        {
            return Ok();
        }

        /// <summary>
        /// Access should only be allowed for admin. Fail.
        /// </summary>
        /// <returns></returns>
        [Route("api/UAC/Admin")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.READ)]
        public IHttpActionResult GetAdmin()
        {
            return Ok();
        }

        /// <summary>
        /// Access should only be allowed for indivual users with all claims. Fail.
        /// </summary>
        /// <returns></returns>
        [Route("api/UAC/Individual")]
        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.DICTIONARY, Operation = ActionConstant.UPDATE)]
        public IHttpActionResult GetTest()
        {
            return Ok();
        }

        /// <summary>
        /// Access should only be allowed for individual users. Pass.
        /// </summary>
        /// <returns></returns>
        [Route("api/UAC/Individual2")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.INDIVIDUAL, Operation = ActionConstant.READ)]
        public IHttpActionResult GetIndividual()
        {
            return Ok();
        }

        /// <summary>
        /// Access should only be allowed for restaurant users. Fail.
        /// </summary>
        /// <returns></returns>
        [Route("api/UAC/Restaurant")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.RESTAURANT, Operation = ActionConstant.READ)]
        public IHttpActionResult GetRestaurant()
        {
            return Ok();
        }
    }
}
