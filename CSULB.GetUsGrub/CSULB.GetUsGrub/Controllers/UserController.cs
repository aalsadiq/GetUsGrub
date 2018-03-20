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
                return BadRequest("Something went wrong. Pleast try again later.");
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

        //TOOD:@Angelica add createuser controller [Angelica]
        // POST Registration/User
        [HttpPost]
        // Opts authentication
        [Route("User/Admin/Create")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        public IHttpActionResult RegisterAdminUser([FromBody] RegisterUserDto registerUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                System.Diagnostics.Debug.WriteLine("herehere");
                System.Diagnostics.Debug.WriteLine("Username: " + registerUserDto.UserAccountDto.Username);
                System.Diagnostics.Debug.WriteLine("Password: " + registerUserDto.UserAccountDto.Password);
                System.Diagnostics.Debug.WriteLine("Dsiplay: " + registerUserDto.UserProfileDto.DisplayName);
                System.Diagnostics.Debug.WriteLine(registerUserDto);
                var userManager = new UserManager();
                var response = userManager.CreateAdmin(registerUserDto);//changed this line...-----
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                //return Ok(registerUserDto.UserAccount.Username);
                // TODO: @Jenn Change to Created [-Jenn]
                return Created("Individual user has been created: ", registerUserDto.UserAccountDto.Username);
            }
            // Catch exceptions
            catch (Exception ex)
            {
                //return BadRequest(ErrorHandler.GetGeneralError());
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Controller that will be called when admin must deactivate a user. 
        /// To access this controller, admin user must have valid claims.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/08/2018
        /// </summary>
        //DELETE AdminHome/DeactivateUser

        //TODO: Add Claims

        //[ResponseType(typeof(UserAccount))]
        //TODO: @Rachel resposne type is a user? What does response type claim do return claims?
        [Route("DeleteUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Reactivate")]
        [HttpDelete]
        //TODO: Add claims
        public IHttpActionResult Delete([FromBody] string username)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest(ModelState);//TODO: @Angelica Extract error [Angelica, Jen]
            }
            try
            {
                var manager = new UserManager();
                var response = manager.DeleteUser(username);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Created("User has been deleted: ", username);
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
        //PUT AdminHome/DeactivateUser
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
                    //var modelState = ActionContext.ModelState;
                    //ActionContext.Response = ActionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, modelState);//Returns the action context
                return BadRequest(ModelState);
                }
                try
                {
                    var manager = new UserManager();//calling appropriate manager.
                    var response = manager.DeactivateUser(username);
                    if (response.Error != null)
                    {
                        return BadRequest(response.Error);
                    }
                return Created("User has been deactivated: ", username);//@Jenn What does this do?
                //return Ok(response);//Returns a deactivated user response.
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); //Returns a message that describes the current exception.
                }
            }
   

        /// <summary>
        /// Controller that will be called when admin must reactivate a user. 
        /// To access this controller, admin user must have valid claims.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/08/2018
        /// </summary>

        //PUT AdminHome/ReactivateUser
        [Route("ReactivateUser")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Reactivate")]
        [HttpPut]
        public IHttpActionResult ReactivateUser([FromBody] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var manager = new UserManager();
                var response = manager.ReactivateUser(username);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Created("User has been reactivated: ", username);//@Jenn What does this do?
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Controller that will be called when admin must edit a user. 
        /// To access this controller, admin user must have valid claims.
        /// @author: Angelica Salas Tovar
        /// @updated: 03/08/2018
        /// </summary>

        //PUT AdminHome/EditUser
        //[Route("EditUser")]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "User", Operation = "Update")]
        //[HttpPut]
        public IHttpActionResult EditUser([FromBody] EditUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var manager = new UserManager();
                var response = manager.edituser(user);

                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }
                return Created("User has been edited: ", user);//@Jenn What does this do?
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
