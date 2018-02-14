using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    public class ReactivateUserController : ApiController
    {

        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Authorize(Roles = "Administrators")]
        [Route("api/ReactivateUser/")]
        public IHttpActionResult Post(Object user)
        {
            return Ok();
        }
    }
}
