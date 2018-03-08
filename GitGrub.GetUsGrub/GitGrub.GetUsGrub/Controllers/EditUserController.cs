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
    /// Controller that will be called when admin must edit a user. 
    /// To access this controller, admin user must have valid claims.
    /// @author: Angelica Salas Tovar
    /// @updated: 03/08/2018
    /// </summary>
    [RoutePrefix("/AdminHome")]
    public class EditUserController : ApiController
    {
        //PUT AdminHome/EditUser
        [Route("EditUser")]
        [HttpPut]
        public IHttpActionResult EditUser([FromBody] RegisterUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var manager = new EditUserController();
                var response = manager.EditUser(user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Controller that will be called when admin must edit restaurant a user. 
        /// To access this controller, admin user must have valid claims.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/08/2018
        /// </summary>
        [Route("EditRestaurant")]
        [HttpPut]
        public IHttpActionResult EditRestaurant([FromBody] RegisterRestaurantUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var manager = new EditUserController();
                var response = manager.EditRestaurant(user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
