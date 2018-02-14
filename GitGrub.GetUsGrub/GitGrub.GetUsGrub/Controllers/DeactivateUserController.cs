using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    public class DeactivateUserController : ApiController
    {

        [Authorize(Roles = "Administrators")]
        [Route("api/DeactivateUser/")]
        [HttpPost]
        public IHttpActionResult deactivateUser(Object user)
        {
            return Ok();
        }

    }
}
