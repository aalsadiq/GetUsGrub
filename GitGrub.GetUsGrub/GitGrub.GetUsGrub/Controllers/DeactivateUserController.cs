using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models.Models;
using GitGrub.GetUsGrub.Models.DTOs;

namespace GitGrub.GetUsGrub.Controllers
{
    public class DeactivateUserController : ApiController
    {
        //Creating the service that will handle the business logic (UserManager.cs)
        public UserManager UserManager { get; }
        //parameterless constructor
        public DeactivateUserController()
        {
        }
        //create a controller for the service...
        public DeactivateUserController(UserManager userManager)
        {
            UserManager = userManager;
        }

        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Route("deactivateuser/admin")]
        [HttpPut]

        public IHttpActionResult DeactivateUser([FromBody] UserManagerDTO userDTO)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("inside invalid model state");
            }

            try
            {
               //UserManager.deactivateUser(userDTO);//call manager/service/business logic

                //call gateway
            }
            catch //not sure what i am catching..
            {
                return BadRequest("Inside catch");
            }

            return Ok(userDTO);//return a request?

        }

        [Route("deactivateuser/admin")]
        [HttpGet]
        public IHttpActionResult getUserById([FromBody] UserManagerDTO userDTO)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("inside invalid model state");
            }

            try
            {
               // UserManager.getUserByID(userDTO);//call manager/service/business logic

                //call gateway
            }
            catch //not sure what i am catching..
            {
                return BadRequest("Inside catch");
            }

            return Ok(userDTO);//return a request?
        }
    }
}
