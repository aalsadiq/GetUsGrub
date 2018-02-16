using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.Controllers
{
    public class EditUserController : ApiController
    {
        //Creating the service that will handle the business logic (UserManager.cs)
        public UserManager UserManager { get; }
        //parameterless constructor
        public EditUserController()
        {
        }
        //create a controller for the service...
        public EditUserController(UserManager userManager)
        {
            UserManager = userManager;
        }
        [Authorize(Roles = "Administrators")]
        [Route("api/deactivateUser/")]
        [HttpPut]
        public IHttpActionResult deactivateUser(User user)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("inside invalid model state");
            }

            try
            {
                UserManager.editUser(user);//call manager/service/business logic

                //call gateway
            }
            catch //not sure what i am catching..
            {
                return BadRequest("Inside catch");
            }e

            return Ok(user);

        }
    }
}
