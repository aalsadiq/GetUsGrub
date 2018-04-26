using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.IdentityModel.Services;
using System.Security.Permissions;
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
    [RoutePrefix("User")]
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
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
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
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
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
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
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
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
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
        // POST User/CreateAdmin
        [HttpPost]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.CREATE)]
        [Route("CreateAdmin")]       
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]       
        public IHttpActionResult RegisterAdminUser([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var userManager = new UserManager();
                var response = userManager.CreateAdmin(registerUserDto);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                return Created("User has been created: ", registerUserDto.UserAccountDto.Username);
            }
            catch (Exception)
            {
                // Sending HTTP response 400 Status
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// Delete User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be deleted.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // DELETE User/DeleteUser
        [HttpDelete]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.DELETE)]
        [Route("DeleteUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "DELETE")]       
        public IHttpActionResult DeleteUser([FromBody] UserAccountDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var manager = new UserManager();
                var response = manager.DeleteUser(user);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("User has been deleted:" + user.Username);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
        /// <summary>
        /// Deactivate User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be reactivated.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // POST User/DeactivateUser
        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.DEACTIVATE)]
        [Route("DeactivateUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "PUT")] 
        public IHttpActionResult DeactivateUser([FromBody] UserAccountDto user)
        {
            //System.Diagnostics.Debug.WriteLine("The user name is "+ user.Username);
            //Checks if what was given is a valid model
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var manager = new UserManager();
                var response = manager.DeactivateUser(user);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("User has been deactivated:" + user.Username);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// Reactivate User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be reactivated.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // POST User/ReactivateUser
        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.REACTIVATE)]
        [Route("ReactivateUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "PUT")]    
        public IHttpActionResult ReactivateUser([FromBody] UserAccountDto user)
        {
            //Checks if what was given is a valid model.
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var manager = new UserManager();
                var response = manager.ReactivateUser(user);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("User has been reactivated:" + user.Username);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// Edit User validates the model state and routes the data transfer object,
        /// </summary>
        /// @author: Angelica Salas Tovar
        /// @updated: 03/20/2018
        /// <param name="username">The expected user to be edited.</param>
        /// <returns>An Http response or Bad Request HTTP resposne.</returns>
        // PUT User/EditUser
        [HttpPut]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = ResourceConstant.USER, Operation = ActionConstant.UPDATE)]
        [Route("EditUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "PUT")]        
        public IHttpActionResult EditUser([FromBody] EditUserDto user)
        {
            System.Diagnostics.Debug.WriteLine("new: " + user.NewUsername + "dis: " + user.NewDisplayName + "user:" +user.Username);
            //Checks if what was given is a valid model.
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                if(user.NewDisplayName=="" && user.NewUsername == "" || user.NewDisplayName==null && user.NewUsername==null)
                {
                    return BadRequest("Invalid: Empty new username or displayname");
                }
                var manager = new UserManager();
                var response = manager.Edituser(user);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("User has been edited: " + user.Username);
            }
            catch (Exception)
            {
                //If any exceptions occur, send an HTTP response 400 status.
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("FirstTimeRegistration/Individual")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RegisterFirstTimeIndividualUser([FromBody] RegisterUserDto registerUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var userManager = new UserManager();
                var response = userManager.CreateFirstTimeIndividualUser(registerUserDto);
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
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("FirstTimeRegistration/Restaurant")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RegisterFirstTimeRestaurantUser([FromBody] RegisterRestaurantDto registerRestaurantDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
            }
            try
            {
                var userManager = new UserManager();
                var response = userManager.CreateFirstTimeRestaurantUser(registerRestaurantDto);
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
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}
