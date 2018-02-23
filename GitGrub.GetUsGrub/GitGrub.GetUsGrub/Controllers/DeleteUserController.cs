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
    [RoutePrefix("api/admin")]
    public class DeleteUserController : ApiController
    {
         /// <summary>
         /// Angelica
         /// </summary>
         /// <param name="User"></param>
         /// <returns></returns>
        //Restructured
        [Route("delete")]
        [HttpDelete]//DeleteRequest
        public IHttpActionResult CreateUser([FromBody] UserDto User)
        {
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
                var manager = new DeleteUserManager();
                var deleteResponse = manager.Delete(User);//calls manager tto delete user

                return Ok(deleteResponse);//Returns the new user...
            }
            catch (CustomException deleteEx)
            {
                return BadRequest(deleteEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //Gets a message that describes the current exception.
            }
        }
    }
}
