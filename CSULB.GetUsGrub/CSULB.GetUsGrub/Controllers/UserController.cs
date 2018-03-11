using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// User controller will handle routes that deal with CRUD.
    /// @author Angelica
    /// </summary>
    [RoutePrefix("User")] //default route
    public class UserController : ApiController
    {
        //TODO: @Jen add create user.

        /// <summary>
        /// Controller that will be called when admin must deactivate a user. 
        /// To access this controller, admin user must have valid claims.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/08/2018
        /// </summary>
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

                //TODO: @Angelica fix manager call

                //var manager = new DeleteUserManager();
                //var response = manager.DeleteUser(username);

                //return Ok(response);
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Controller that will be called when admin must deactivate a user. 
        /// To access this controller, admin user must have valid claims.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/08/2018
        /// </summary>
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
                    return Ok("Hi Ryan and Jen");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); //Returns a message that describes the current exception.
                }
            }
        }

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
            //[Route("EditUser")]
            //[HttpPut]
            //public IHttpActionResult EditUser([FromBody] RegisterUserDto user)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //    try
            //    {
            //        var manager = new EditUserController();
            //        var response = manager.EditUser(user);

            //        return Ok(response);
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest(ex.Message);
            //    }
            //}
            /// <summary>
            /// Controller that will be called when admin must edit restaurant a user. 
            /// To access this controller, admin user must have valid claims.
            /// @author: Angelica Salas Tovar
            /// @updated: 03/08/2018
            /// </summary>
            //[Route("EditRestaurant")]
            //[HttpPut]
            //public IHttpActionResult EditRestaurant([FromBody] RegisterRestaurantUserDto user)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }
            //    try
            //    {
            //        var manager = new EditUserController();
            //        var response = manager.EditRestaurant(user);

            //        return Ok(response);
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest(ex.Message);
            //    }
            //}
        }
    }
}
