using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    /// <summary>
    /// Controller that will be called when admin must reactivate a user. 
    /// To access this controller, admin user must have valid claims.
    /// @author: Angelica Salas Tovar
    /// @updated: 03/08/2018
    /// </summary>
    [RoutePrefix("/AdminHome")]
    public class ReactivateUserController : ApiController
    {
        //PUT AdminHome/ReactivateUser
        [Route("ReactivateUser")]
        [HttpPut]
        public IHttpActionResult ReactivateUser([FromBody] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var manager = new ReactivateUserController();
                var response = manager.ReactivateUser(username);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
