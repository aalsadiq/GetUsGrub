using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GitGrub.GetUsGrub.Models;

namespace GitGrub.GetUsGrub.Controllers
{
    public class CreateUserController : ApiController
    {

        //public CreateUserController()
        //{

        //}





        //[Route("api/getcreateuser")]
        //[HttpGet]
        //public HttpResponseMessage get                createuser(user user)
        //{
        //    var result = _companyrepository.getall();
        //    httpresponsemessage response = request.createresponse(httpstatuscode.ok, result);
        //    return response;
        //}

        [Route("api/CreateUserController")]//this is the route that will allow creation
        [HttpPost]
        public IHttpActionResult executecreateuser(User createuserrequest)//Response is the type of HTTP request...d
        {
            if (createuserrequest == null)
            {
                return BadRequest("invalid");
                //return notfound();
            }

            return Ok(createuserrequest);
        }

        //[Route("api/CreateUserController")]//this is the route that will allow creation
        [Route("api/CreateUserController/show")]
        [HttpGet]
        public IHttpActionResult getCreateUser(User createuserrequest)//Response?
        {
            if (createuserrequest == null)
            {
                return BadRequest("invalid");
                //return notfound();
            }

            return Ok(createuserrequest);
        }

        //public void post ([FromBody])

    }
}
