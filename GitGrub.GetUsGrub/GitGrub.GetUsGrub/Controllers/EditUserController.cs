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
            try
            {
                //logic for implementing update the user on Database...
                return Ok();
            }
            catch
            {
                return InternalServerError();
            }

            //Http Server
            //HttpRoutingDispatcher
            //HttpControllerDispatcher
            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/http-message-handlers
        }
    }
}
