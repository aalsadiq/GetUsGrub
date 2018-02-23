using GitGrub.GetUsGrub.Managers;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    /// <summary>
    /// Angelica
    /// </summary>
    //Restructured
    [RoutePrefix("api/admin")]
    public class DeactivateUserController : ApiController
    {
        [Route("deactivate")]
        [HttpPut]
        public IHttpActionResult DeactivateUser([FromBody] UserDto User)
        {
            //if the model is not valid return a bad request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Validating user length
                var accountValidator = new UserAccountValidator();
                //accountValidator.validateUsernameLength(User.Username);

                //Call Manager 
                var manager = new DeactivateUserManager();
                var deactivation = manager.Deactivate(User);//calls manager to deactivate this user
                
                return Ok(deactivation);//Returns the new user...
            }
            catch (CustomException deactivateException)
            {
                return BadRequest(deactivateException.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); //Gets a message that describes the current exception.
            }
        }
    }
}
