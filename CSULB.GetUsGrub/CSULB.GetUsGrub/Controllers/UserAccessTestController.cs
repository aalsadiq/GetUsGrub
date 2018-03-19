using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Security.Permissions;
using System.Threading;
using System.Web.Http;

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
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Read")]
        public IHttpActionResult GetAdmin()
        {
            return Ok();
        }

        [Route("api/UAC/Admin/2")]
        [HttpGet]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        public IHttpActionResult GetTest()
        {
            return Ok();
        }

        [Route("api/UAC/Individual")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Individual", Operation = "Read")]
        public IHttpActionResult GetIndividual()
        {
            return Ok();
        }

        [Route("api/UAC/Restaurant")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "Restaurant", Operation = "Read")]
        public IHttpActionResult GetRestaurant()
        {
            return Ok();
        }
    }
}
