using GitGrub.GetUsGrub.BusinessLogic.Managers;
using GitGrub.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    /// <summary>
    /// Controller that will be called when admin must deactivate a user. 
    /// To access this controller, admin user must have valid claims.
    /// @author: Angelica Salas Tovar
    /// @updated: 03/08/2018
    /// </summary>
    [RoutePrefix("/AdminHome")]
    public class DeactivateUserController : ApiController
    {
        //PUT AdminHome/DeactivateUser
        [Route("DeactivateUser")]
        [HttpPut]
        //TODO: Add claims here
        public IHttpActionResult DeactivateUser([FromBody] string username)
        {
            //if the model is not valid return a bad request
            if (!ModelState.IsValid)//was binding successful?
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var manager = new DeactivateUserManager();//calling appropriate manager.
                //var response = manager.DeactivateUser(username);

                //return Ok(response);//Returns a deactivated user response.
                return Ok("Hi Ryan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //Returns a message that describes the current exception.
            }
        }
    }
}
