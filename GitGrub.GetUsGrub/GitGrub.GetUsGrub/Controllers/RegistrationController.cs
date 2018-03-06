using GitGrub.GetUsGrub.BusinessLogic;
using GitGrub.GetUsGrub.Models;
using System;
using System.Web.Http;

namespace GitGrub.GetUsGrub
{
    /// <summary>
    /// The <c>RegistrationController</c> class.
    /// Contains Post action methods for registering a user.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    [RoutePrefix("Registration")]
    public class RegistrationController : ApiController
    {
        // TODO: Add in AntiForgery Token Validator
        /*
         * OWASP definition:
         * Cross-Site Request Forgery (CSRF) is an attack that forces an end user
         * to execute unwanted actions on a web application which they are currently
         * authenticated.
         * CSRF attacks specifically target state-changing requests, not theft of data,
         * since the attacker has no way to see the response to the forged request.
         */

        /// <summary>
        /// The RegisterUserAccount method.
        /// Binds data and validates data transfer object containing models
        /// and calls the CreateUserManager class to perform business logic.
        /// Returns "Ok" result if request if fully processed successfully.
        /// Returns "BadRequest" result if any error occurred.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        // POST Registration/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("User")]
        public IHttpActionResult RegisterUserAccount([FromBody] RegisterUserDto registerUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registerUserPreLogicStrategy = new RegisterUserPreLogicValidationStrategy();
                var createUserManager = new CreateUserManager();
                //System.Diagnostics.Debug.WriteLine("here0");
                var responseDto = registerUserPreLogicStrategy.RunValidators(registerUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                //System.Diagnostics.Debug.WriteLine("here1");
                responseDto = createUserManager.CheckUserDoesNotExist(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                //System.Diagnostics.Debug.WriteLine("here2");
                responseDto = createUserManager.HashPassword(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                //System.Diagnostics.Debug.WriteLine("here3");
                responseDto = createUserManager.HashSecurityAnswers(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                //System.Diagnostics.Debug.WriteLine("here4");
                responseDto = createUserManager.CreateClaims(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                //System.Diagnostics.Debug.WriteLine("here5");
                responseDto = createUserManager.SetAccountIsActive(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                //System.Diagnostics.Debug.WriteLine("here6");
                // TODO: PostLogic Validation
                responseDto = createUserManager.CreateNewUser(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                // TODO: Change this to show a message with the username
                //return Ok(registerUserDto.UserAccount.Username);
                return Ok(responseDto.Data);
            }
            // Catch exceptions
            catch (Exception ex)
            {
                //return BadRequest(ErrorHandler.GetGeneralError());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// The RegisterRestaurantUserAccount method.
        /// Binds data and validates data transfer object containing models
        /// and calls the CreateUserManager class and CreateRestaurantUserManager to perform business logic.
        /// Returns "Ok" result if request if fully processed successfully.
        /// Returns "BadRequest" result if any error occurred.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2017
        /// </para>
        /// </summary>
        // POST Registration/Restaurant
        [HttpPost]
        [AllowAnonymous]
        [Route("Restaurant")]
        public IHttpActionResult RegisterRestaurantAccount([FromBody] RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registerUserPreLogicStrategy = new RegisterUserPreLogicValidationStrategy();
                var createRestaurantUserManager = new CreateRestaurantUserManager();
                var createUserManager = new CreateUserManager();

                var responseDto = registerUserPreLogicStrategy.RunValidators(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.CheckUserDoesNotExist(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.HashPassword(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.HashSecurityAnswers(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.CreateClaims(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.SetAccountIsActive(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.CreateNewUser(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                var createRestaurantUserResponseDto = createRestaurantUserManager.CreateNewUser(registerRestaurantUserDto);
                if (createRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(createRestaurantUserResponseDto.Error);
                }
                // TODO: Change this to show a message with the username
                //return Ok(registerRestaurantUser.UserAccount.Username);
                return Ok(responseDto.Data);
            }
            // Catch exceptions
            catch (Exception)
            {
                return BadRequest(ErrorHandler.GetGeneralError());
            }
        }
    }
}
