using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    public class DeleteUserController : ApiController
    {
        [Authorize(Roles = "Administrators")]
        [Route("api/admin/delete/")]
        public IHttpActionResult Post(Object user)
        {
            try
            {
                //logic for implementing delete the user on Database...
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
