using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GitGrub.GetUsGrub.Models.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GitGrub.GetUsGrub.Models.DTOs;



namespace GitGrub.GetUsGrub.Controllers

{
    public class CreateUserController : ApiController
    {
        UserManager userManager = new UserManager();

        //[Authorize(Roles = "Administrators")]//restricting by role --claim = isadmin
        [Route("Admin/CreateUser")]
        [HttpPost]

        public IHttpActionResult CreateUser([FromBody] UserManagerDTO userDTO)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("invalid inputs.");
            }
            try
            {
                var createUserResult = userManager.createUser(userDTO);
                if (createUserResult == true)
                {
                    return Ok("Success");//return a request?
                }
                else
                {
                    return BadRequest("User Creation failed.");
                }
            }
            catch
            {
                return BadRequest("Something went wrong! Please contaact an Admin.");
            }
        }
    }
}


      