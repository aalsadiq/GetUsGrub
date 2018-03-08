using GitGrub.GetUsGrub.BusinessLogic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    /// <summary>
    /// Controller that will be called when admin must delete a user. 
    /// To access this controller, admin user must have valid claims.
    /// @author: Angelica Salas Tovar
    /// @updated: 03/08/2018
    /// </summary>
    [RoutePrefix("/AdminHome")]
    public class DeleteUserController : ApiController
    {
        //DELETE AdminHome/DeactivateUser
        [Route("DeleteUser")]
        [HttpDelete]
        //TODO: Add claims
        public IHttpActionResult DeactivateUser([FromBody] string username)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest(ModelState);
            }
            try
            {
                var manager = new DeleteUserManager();
                var response = manager.DeleteUser(username);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
