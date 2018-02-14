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
        //Template... layout
        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Authorize(Roles = "Administrators")]
        [Route("api/ReactivateUser/")]
        public IHttpActionResult Post(Object user)
        {
            try
            {
                //logic for implementing delete the user on Database... call Manager, factory, gateway
                return Ok();
            }
            catch
            {
                //return InternalServerError();
                return BadRequest();
            }
        }
    }
}
