using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace GitGrub.GetUsGrub.Controllers
{
    //Restructured
    /// <summary>
    /// Angelica
    /// Please note this was restructed several times in other branches.
    /// </summary>
    [RoutePrefix("api/admin")]
    public class CreateUserController : ApiController
    {
        //[Authorize(Roles = "Admin")]
        [Route("create")]
        [HttpPost]//Requesting data..
        public IHttpActionResult CreateUser([FromBody] RegisterUserDto userDTO)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("invalid inputs.");
            }
            try
            {
                //reroute to registrationcontroller or create own because other one allows 
            }
            catch
            {
                
            }
            //Rereoute to Registration controller
            // return RedirectToRoute("api/registration/admin/create", userDTO);
            return Ok;
        }

    }
}
