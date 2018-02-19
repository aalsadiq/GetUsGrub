using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models.Models;
namespace GitGrub.GetUsGrub.Controllers
{
    public class CreateUserController : ApiController
    {
        //Creating the service that will handle the business logic (UserManager.cs)
        UserManager userManager = new UserManager();

        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Route("api/CreateUser/")]
        [HttpPost]
        public IHttpActionResult CreateUser([FromBody] Object user)
        {
            return Ok();
        }
    }
}


      