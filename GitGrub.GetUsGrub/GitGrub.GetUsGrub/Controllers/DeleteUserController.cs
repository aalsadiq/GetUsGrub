using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models.Models;
using GitGrub.GetUsGrub.Models.DTOs;
//added
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GitGrub.GetUsGrub.Controllers
{
    public class DeleteUserController : ApiController
    {
        //Creating the service that will handle the business logic (UserManager.cs)
        public UserManager UserManager { get; }
        //parameterless constructor
        public DeleteUserController()
        {
        }
        //create a controller for the service...
        public DeleteUserController(UserManager userManager)
        {
            UserManager = userManager;
        }

        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Route("deleteuser/admin")]
        [HttpDelete]

        public IHttpActionResult DeleteUser([FromBody] UserManagerDTO userDTO)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("inside invalid model state");
            }

            try
            {
                UserManager.deleteUser(userDTO);//call manager/service/business logic

                //call gateway
            }
            catch //not sure what i am catching..
            {
                return BadRequest("Inside catch");
            }

            return Ok(userDTO);//return a request?

        }
        //read, because you want to see the user you will delete before deleting
        //[Route("userbeforedelete/admin")]
        //[HttpGet]
       
    }
}
