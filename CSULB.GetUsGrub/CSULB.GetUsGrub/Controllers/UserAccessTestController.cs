using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading;
using System.Web.Http;

using CSULB.GetUsGrub.UserAccessControl;

namespace CSULB.GetUsGrub
{
    public class UserAccessTestController : ApiController
    {
        public UserAccessTestController()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("username", "Admin")
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "test");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            Thread.CurrentPrincipal = principal;
        }

        [AllowAnonymous]
        [Route("api/UAC")]
        public IHttpActionResult GetAnon()
        {
            return Ok();
        }

        [Route("api/UAC/Admin")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.READ)]
        public IHttpActionResult GetAdmin()
        {
            return Ok();
        }

        [Route("api/UAC/Admin/2")]
        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.UPDATE)]
        public IHttpActionResult GetTest()
        {
            return Ok();
        }

        [Route("api/UAC/Individual")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.INDIVIDUAL, Operation = ActionConstant.READ)]
        public IHttpActionResult GetIndividual()
        {
            return Ok();
        }

        [Route("api/UAC/Restaurant")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.RESTAURANT, Operation = ActionConstant.READ)]
        public IHttpActionResult GetRestaurant()
        {
            return Ok();
        }
    }
}
