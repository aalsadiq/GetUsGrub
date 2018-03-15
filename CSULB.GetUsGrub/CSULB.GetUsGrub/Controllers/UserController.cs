using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Permissions;
using System.Web.Http;
using System.Web.Http.Description;
//using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
    /// <summary>
    /// User controller will handle routes that deal with CRUD.
    /// @author Angelica
    /// </summary>
    
    [RoutePrefix("User")] //default route
    public class UserController : ApiController
    {
        //TODO: @Jenn Add create individual user controller. [-Angelica]
        // POST Registration/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Registration/Individual")]
        //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
        public IHttpActionResult RegisterIndividualUser([FromBody] RegisterUserDto registerUserDto)
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
                var response = userManager.CreateIndividualUser(registerUserDto);
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

        //TODO: @Jenn Add create individual user controller. [-Angelica]
        // POST Registration/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Registration/Restaurant")]
        //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
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
                //return Ok(registerUserDto.UserAccount.Username);
                // TODO: @Jenn Change to Created [-Jenn]
                return Created("Restaurant user has been created: ", registerRestaurantDto.UserAccountDto.Username);
            }
            // Catch exceptions
            catch (Exception ex)
            {
                //return BadRequest(ErrorHandler.GetGeneralError());
                return BadRequest(ex.Message);
            }
        }

        //TOOD:@Angelica add createuser controller [Angelica]
        // POST Registration/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("User/Admin/Create")]
        //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
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
       // [ClaimsPrincipalPermission(SecurityAction.Demand = "User", Operation = "Delete")]
        [HttpDelete]
        //TODO: Add claims
        public IHttpActionResult Delete([FromBody] string username)
        {
            if (!ModelState.IsValid)//if the model is not valid return a bad request
            {
                return BadRequest(ModelState);
            }
            try
            {
                var manager = new UserManager();
                var response = manager.DeleteUser(username);

                //if (response.Error != null)
                //{
                //    return BadRequest(response.Error);
                //}

                return Ok(response);

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
                    var manager = new UserManager();//calling appropriate manager.
                    var response = manager.DeactivateUser(username);

                    return Ok(response);//Returns a deactivated user response.
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

                    return Ok(response);
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
                var manager = new UserManager();
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
            public IHttpActionResult EditRestaurant([FromBody] RegisterRestaurantDto user)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                try
                {
                    var manager = new UserManager();
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
