using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    public class EditUserController : ApiController
    {
        [Authorize(Roles = "Administrators")]
        [Route("api/EditUser/")]
        public IHttpActionResult Post(Object user)
        {
            return Ok();

        }
    }
}
