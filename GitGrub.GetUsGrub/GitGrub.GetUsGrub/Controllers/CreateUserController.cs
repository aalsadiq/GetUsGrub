using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace GitGrub.GetUsGrub.Controllers
{
    public class CreateUserController : ApiController
    {
        //Creating the service that will handle the business logic (UserManager.cs)
        public UserManager UserManager { get; }
        //parameterless constructor
        public CreateUserController()
        {
        }
        //create a controller for the service...
        public CreateUserController(UserManager userManager)
        {
            UserManager = userManager;
        }

        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Route("api/CreateUser/")]
        [HttpPost]
        public IHttpActionResult CreateUser([FromBody] User user)
        {
            if(!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("inside invalid model state");    
            }

            try
            {
                UserManager.createUser(user);//call manager/service/business logic

                //call gateway
            }
            catch //not sure what i am catching..
            {
                return BadRequest("Inside catch");
            }

            return Ok(user);

        }

    }
}


      