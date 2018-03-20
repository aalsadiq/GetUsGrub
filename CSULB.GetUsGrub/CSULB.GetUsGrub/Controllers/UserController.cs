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
    /// The <c>UserController</c> class.
    /// User controller will handle routes that deal with creating, updating, reading and deleting a user.
    /// <para>
    /// @author: Angelica Salas Tovar, Jennifer Nguyen
    /// @updated: 03/13/2018
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
        // POST Registration/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Registration/Individual")]
        // TODO: @Jenn Test out the methods with POST [-Jenn]
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
        // POST Registration/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Registration/Restaurant")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
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
        /// The RegisterIndividualUser method.
        /// Validates model state and routes the data transfer object.
        /// <para>
        /// @author: Jennifer Nguyen, Angelica Salas
        /// @updated: 03/20/2018
        /// </para>
        /// </summary>
        /// <param name="registerUserDto">The user information that will be stored in the database.</param>
        /// <returns>Created HTTP response or Bad Request HTTP response</returns>
        // POST User/Admin/Create
        [HttpPost]
        // Opts authentication
        [Route("User/Admin/Create")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        public IHttpActionResult RegisterAdminUser([FromBody] RegisterUserDto registerUserDto)
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
                var response = userManager.CreateAdmin(registerUserDto);//Calls create admin method from usermanager.
                if (response.Error != null)
                {
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
        // POST User/Admin/DeleteUser
        [Route("DeleteUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Reactivate")]
        [HttpDelete]
        public IHttpActionResult DeleteUser([FromBody] string username)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest("Something went wrong, please try again later");//TODO: @Angelica Extract error [Angelica, Jen]
            }
            try
            {
                var manager = new UserManager();
                var response = manager.DeleteUser(username);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("User has been deleted:" + username);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Deactivate")]
        [HttpPut]
            //TODO: Add claims here
            public IHttpActionResult DeactivateUser([FromBody] string username)
            {
                //if the model is not valid return a bad request
                if (!ModelState.IsValid)//was binding successful?
                {
                    return BadRequest("Something went wrong, please try again later");
                }
                try
                {
                    var manager = new UserManager();//calling appropriate manager.
                    var response = manager.DeactivateUser(username);
                    if (response.Error != null)
                    {
                        return BadRequest(response.Error);
                    }
                    return Ok("User has been deactivated:" + username);//Returns a deactivated user response.
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); //Returns a message that describes the current exception.
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
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Reactivate")]
        [HttpPut]
        public IHttpActionResult ReactivateUser([FromBody] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong, please try again later");
            }
            try
            {
                var manager = new UserManager();
                var response = manager.ReactivateUser(username);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Ok("User has been reactivated:" + username);//Returns reactivated username
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        [HttpPut]
        public IHttpActionResult EditUser([FromBody] EditUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong, please try again later");
            }
            try
            {
                var manager = new UserManager();
                var response = manager.edituser(user);

                if (response.Error != null)//If manager has an error.
                {
                    return BadRequest(response.Error);//If manager fails, it returns a bad request which is then caught in the exception.
                }
                return Ok("User has been edited:" + user.Username);//Returns edited username
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);//Http Resposne 400
            }
        }
    }
}
