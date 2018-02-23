using GitGrub.GetUsGrub.Managers;//Angelica
using GitGrub.GetUsGrub.Models.DTOs;//Angelica
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    [RoutePrefix("api/admin")]
    public class ReactivateUserController : ApiController
    {
        /// <summary>
        /// Reactivate
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        //Restructured
        [Route("reactivate")]
        [HttpPut]
        public IHttpActionResult ReactivateUser([FromBody] UserDto User)
        {
            //if the model is not valid return a bad request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //call strategy (running validations)

                //call manager
                var reactivateUserManager = new ReactivateUserManager();

                //chekc if user does not exist
                reactivateUserManager.checkIfUserDoesNotExist(User.Username);
                reactivateUserManager.Reactivate(User);

            }
            catch (Exception createUserException)
            {
                return BadRequest(createUserException.Message);
            }
            return Ok(User);
        }
    }
}
