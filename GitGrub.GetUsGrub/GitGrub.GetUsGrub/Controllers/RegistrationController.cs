using GitGrub.GetUsGrub.BusinessLogic;
using GitGrub.GetUsGrub.Models;
using System;
using System.Web.Http;

namespace GitGrub.GetUsGrub
{
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
        // POST Registration/user
        [HttpPost]
        // AllowAnonymous opts out authentication for the user
        [AllowAnonymous]
        [Route("User")]
        public IHttpActionResult RegisterUserAccount([FromBody] RegisterUserDto registerUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                // TODO: Implement maybe a handler to parse message to something cleaner
                return BadRequest(ModelState);
            }
            try
            {
                var registerUserPreLogicStrategy = new RegisterUserPreLogicStrategy();
                var createUserManager = new CreateUserManager();
                System.Diagnostics.Debug.WriteLine("here0");
                var responseDto = registerUserPreLogicStrategy.RunValidators(registerUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                System.Diagnostics.Debug.WriteLine("here1");
                responseDto = createUserManager.CheckUserDoesNotExist(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                System.Diagnostics.Debug.WriteLine("here2");
                responseDto = createUserManager.HashPassword(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                System.Diagnostics.Debug.WriteLine("here3");
                responseDto = createUserManager.HashSecurityAnswers(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                System.Diagnostics.Debug.WriteLine("here4");
                responseDto = createUserManager.CreateClaims(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                System.Diagnostics.Debug.WriteLine("here5");
                responseDto = createUserManager.SetAccountIsActive(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                System.Diagnostics.Debug.WriteLine("here6");
                // TODO: PostLogic Validation
                responseDto = createUserManager.CreateNewUser(responseDto.Data);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                System.Diagnostics.Debug.WriteLine("here7");
                // TODO: Change this to show a message with the username
                return Ok(responseDto.Data);

            }
            // Catch exceptions
            catch (Exception ex)
            {
                // TODO: Should this be the string?
                //return BadRequest(ErrorHandler.SetGeneralError());
                return BadRequest(ex.Message);
            }
        }

        // POST Registration/restaurant
        [HttpPost]
        [AllowAnonymous]
        [Route("Restaurant")]
        public IHttpActionResult RegisterRestaurantAccount([FromBody] RegisterRestaurantUserDto registerRestaurantUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                // ***Implement maybe a handler to parse message to something cleaner
                return BadRequest(ModelState);
            }
            try
            {
                var registerUserPreLogicStrategy = new RegisterUserPreLogicStrategy();
                var createRestaurantUserManager = new CreateRestaurantUserManager();
                var createUserManager = new CreateUserManager();

                // Make the validations extensible for registerRestaurantUser
                //var responseDto = registerUserPreLogicStrategy.RunValidators(registerRestaurantUserDto);
                //if (responseDto.Error != null)
                //{
                //    return BadRequest(responseDto.Error);
                //}
                var responseDto = createUserManager.CheckUserDoesNotExist(registerRestaurantUserDto);
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
                var createRestaurantUserResponseDto = createRestaurantUserManager.CreateNewUser(registerRestaurantUserDto);
                if (createRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(createRestaurantUserResponseDto.Error);
                }
                // TODO: Change this to show a message with the username
                return Ok(responseDto.Data);
            }
            // Catch exceptions
            catch (Exception)
            {
                // TODO: Should this be the string?
                return BadRequest(ErrorHandler.SetGeneralError());
            }
        }
    }
}
