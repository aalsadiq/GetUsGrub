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
                // TODO: System.Diagnostics.Debug.WriteLine("here");
                var registerUserPreLogicStrategy = new RegisterUserPreLogicStrategy();
                var createUserManager = new CreateUserManager();

                var responseDto = registerUserPreLogicStrategy.RunValidators(registerUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.CheckUserDoesNotExist(registerUserDto.Username);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.HashPassword(registerUserDto.Password);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                // TODO: Hash Security Answers
                responseDto = createUserManager.CreateClaims();
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.SetAccountIsActive();
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                // TODO: PostLogic Validation
                responseDto = createUserManager.CreateNewUser(registerUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }

                // TODO: Change this to show a message with the username
                return Ok(responseDto.Data);

            }
            // Catch exceptions
            catch (Exception)
            {
                // TODO: Should this be the string?
                return BadRequest("Something went wrong. Please try again later.");
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
                var responseDto = registerUserPreLogicStrategy.RunValidators(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.CheckUserDoesNotExist(registerRestaurantUserDto.Username);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                responseDto = createUserManager.HashPassword(registerRestaurantUserDto);
                if (responseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                // TODO: Hash Security Answers
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
                var restaurantResponseDto = createRestaurantUserManager.CreateNewUser(registerRestaurantUserDto);
                if (restaurantResponseDto.Error != null)
                {
                    return BadRequest(responseDto.Error);
                }
                // Change this to show a message with the username
                return Ok(restaurantResponseDto.Data);
            }
            // Catch exceptions
            catch (Exception)
            {
                // TODO: Should this be the string?
                return BadRequest("Something went wrong. Please try again later.");
            }
        }
    }
}
