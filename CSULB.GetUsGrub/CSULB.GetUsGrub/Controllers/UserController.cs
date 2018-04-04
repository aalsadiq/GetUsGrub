using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// User controller will handle routes that deal with creating, reading, updating and deleting a user.
    /// <para>
    /// @author: Angelica Salas Tovar, Jennifer Nguyen
    /// @updated: 03/30/2018
    /// </para>
    /// </summary>
    [RoutePrefix("User")] //default route
    public class UserController : ApiController
    {
        /// <summary>
        /// The RegisterIndividualUser method.
        /// Validates model state and routes the data transfer object.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="registerUserDto"></param>
        /// <returns>Created HTTP response or Bad Request HTTP response</returns>
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Registration/Individual")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RegisterIndividualUser([FromBody] RegisterUserDto registerUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                // TODO: @Jenn Parse the ModelState BadRequest to something better [-Jenn]
                return BadRequest(ModelState);
            }
            try
            {
                var userManager = new UserManager();
                var response = userManager.CreateIndividualUser(registerUserDto);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                // Sending HTTP response 201 Status
                return Created("Individual user has been created: ", registerUserDto.UserAccountDto.Username);
            }
            // Catch exceptions
            catch (Exception)
            {
                // Sending HTTP response 400 Status
                return BadRequest("Something went wrong. Please try again later.");
            }
        }

        /// <summary>
        /// The RegisterRestaurantUser method.
        /// Validates the model state and routes the data transfer object.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="registerRestaurantDto"></param>
        /// <returns>Created HTTP response or Bad Request HTTP response</returns>
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Registration/Restaurant")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RegisterRestaurantUser([FromBody] RegisterRestaurantDto registerRestaurantDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userManager = new UserManager();
                var response = userManager.CreateRestaurantUser(registerRestaurantDto);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                // HTTP 201 Status
                return Created("Restaurant user has been created: ", registerRestaurantDto.UserAccountDto.Username);
            }
            // Catch exceptions
            catch (Exception)
            {
                // HTTP 400 Status
                return BadRequest("Something went wrong. Please try again later.");
            }
        }

        /// <summary>
        /// The RegisterAdminUser method.
        /// Validates model state and routes the data transfer object.
        /// <para>
        /// @author: Jennifer Nguyen, Angelica Salas
        /// @updated: 03/20/2018
        /// </para>
        /// </summary>
        /// <param name="registerUserDto">The user information that will be stored in the database.</param>
        /// <returns>Created HTTP response or Bad Request HTTP response</returns>
        // POST User/Admin/Create

        // Opts authentication
        [Route("CreateAdmin")]
        // TODO: @Angelica Change methods to POST [-Jenn]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult RegisterAdminUser([FromBody] RegisterUserDto registerUserDto)
        {
            // Model Binding Validation
            //Checks if what was given is a valid model
            if (!ModelState.IsValid)
            {
                //If mode is invalid, return a bad request.
                return BadRequest("Invalid input, please try again.");
            }
            try
            {
                //Creating a manager to then call CreateAdmin
                var userManager = new UserManager();
                //Calls create admin method from usermanager.
                var response = userManager.CreateAdmin(registerUserDto);
                //Checks the response from CreateAdmin. if Error is null, then it was successful!
                if (response.Error != null)
                {
                    //Will return a bad request if error occured in manager.
                    return BadRequest(response.Error);
                }
                // Sending HTTP response 201 Status
                return Created("User has been created: ", registerUserDto.UserAccountDto.Username);
            }
            // Catch exceptions
            catch (Exception)
            {
                // Sending HTTP response 400 Status
                return BadRequest("Something went wrong. Please try again later.");
            }
        }

        /// <summary>
        /// Delete User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be deleted.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // DELETE User/Admin/DeleteUser
        [Route("DeleteUser")]
        // TODO: @Angelica Change methods [-Jenn]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
       // [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Reactivate")]
        [HttpDelete]
        public IHttpActionResult DeleteUser([FromBody] UserAccountDto user)
        {
            if (user == null)
            {
                return Ok(user);
            }
            //Checks if what was given is a valid model
            if (!ModelState.IsValid)
            {
                //If mode is invalid, return a bad request.
                return BadRequest("Invalid username.");
            }
            try
            {
                //Creating a manager to then call DeleteUser
                var manager = new UserManager();
                //Calling DeleteUser method to delete the username that was received.
                var response = manager.DeleteUser(user.Username);
                //Checks the response from DeleteUser. If Error is null, then it was successful!
                if (response.Error != null)
                {
                //Will return a bad request if error occured in manager.
                    return BadRequest(response.Error);
                }
                //If DeleteUser was successful return HTTP response with a successful message.
                return Ok("User has been deleted:" + user.Username);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                //return BadRequest("Something went wrong. Please try again later.");
                return Ok(user);
            }
        }

        /// <summary>
        /// Deactivate User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be reactivated.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // POST User/Admin/DeactivateUser
        [Route("DeactivateUser")]
        // TODO: @Angelica Change methods [-Jenn]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Deactivate")]
        [HttpPut]
            //TODO: Add claims here
            public IHttpActionResult DeactivateUser([FromBody] UserAccountDto user)
            {
            //System.Diagnostics.Debug.WriteLine("The user name is "+ user.Username);
            //Checks if what was given is a valid model
            if (!ModelState.IsValid)
            {
                //If mode is invalid, return a bad request.
                return BadRequest("The username " + user.Username);
            }
            try
                {
                    //Creating a manger to then call Deactivateuser
                    var manager = new UserManager();
                    //Calling ReactivateUser method to reactivate the username that was recieved.
                    var response = manager.DeactivateUser(user.Username);
                    //Checks the response from ReactivateUser. If Error is null, then it was successful.
                    if (response.Error != null)
                    {
                        //Will return a bad request if error occured in manager.
                        return BadRequest(response.Error);
                    }
                    //IF ReactivateUser was successful return HTTP response with a successful message.
                    return Ok("User has been deactivated:" + user.Username);
                }
                catch (Exception)
                {
                    //If any exceptions occur, send an HTTP response 400 status.
                    return BadRequest("Something went wrong. Please try again later.");
                }
            }


        /// <summary>
        /// Reactivate User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be reactivated.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // POST User/Admin/ReactivateUser
        [Route("ReactivateUser")]
        // TODO: @Angelica Change methods [-Jenn]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Reactivate")]
        [HttpPut]
        public IHttpActionResult ReactivateUser([FromBody] UserAccountDto user)
        {
            //Checks if what was given is a valid model.
            if (!ModelState.IsValid)
            {
                //If model is invalid, return a bad request.
                return BadRequest("Something went wrong, please try again later");
            }
            try
            {
                //Creating a manager to then call ReactivateUser
                var manager = new UserManager();
                //Calling ReactivateUser method to reactivate the username that was recieved.
                var response = manager.ReactivateUser(user);
                //Checks the response from ReactivateUser. IF error is null, then it was successful.
                if (response.Error != null)
                {
                    //Will return a bad request if error occured in manager.
                    return BadRequest(response.Error);
                }
                //If ReactivateUser was successful return HTTP response with a successful message.
                return Ok("User has been reactivated:" + user.Username);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest("Something went wrong. Please try again later.");
            }
        }

        /// <summary>
        /// Edit User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be edited.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // POST User/Admin/EditUser
        [Route("EditUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        [HttpPut]
        public IHttpActionResult EditUser([FromBody] EditUserDto user)
        {
            Debug.WriteLine("username" + user.Username);
            Debug.WriteLine("new username" + user.NewUsername);
            Debug.WriteLine("new displayname" + user.NewDisplayName);
            //Checks if what was given is a valid model.
            if (!ModelState.IsValid)
            {
                //If model is invalid, return a bad request.
                return BadRequest("Something went wrong, please try again later");
                //return Ok(user);
            }
            try
            {
                //Creating a manager to then call EditUser.
                var manager = new UserManager();
                //Calling EditUser method to edit the given user.
                var response = manager.Edituser(user);
                //Checks the response from EditUser. If error is null, then it was successful.
                if (response.Error != null)
                {
                    //Will return a bad request if error occured in manager.
                    return BadRequest(response.Error);
                }
                //If EditUser was successful return HTTP response with a successful message.
                return Ok("User has been edited:" + user.Username);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest("This is a bad request.");
            }
        }
    }
}
