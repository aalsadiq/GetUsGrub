using GitGrub.GetUsGrub.Models;
using System;
using System.Web.Http;

namespace GitGrub.GetUsGrub
{
    [RoutePrefix("api/registration")]
    public class RegistrationController : ApiController
    { 
        // POST api/registration/user
        [HttpPost]
        // AllowAnonymous opts out authentication for the user
        [AllowAnonymous]
        [Route("user")]
        public IHttpActionResult RegisterUserAccount([FromBody] RegisterUserDto registerUserDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                // ***Implement maybe a handler to parse message to something cleaner
                return BadRequest(ModelState);
            }
            try
            {
                var registerUserResponseDto = ResponseDtoFactory.Create(registerUserDto);
                var passwordSalt = new PasswordSalt();
                var createUserManager = new CreateUserManager();

                createUserManager.CheckIfUserExists(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }
                createUserManager.HashPassword(registerUserResponseDto, passwordSalt);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }
                createUserManager.CreateNewUser(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }
                createUserManager.CreateClaims(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }

                return Ok(registerUserResponseDto.Data);

            }
            // Catch system exceptions and custom exceptions.
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //return BadRequest(ControllerErrorHandler.SetGeneralError());
                return BadRequest(ex.Message);
            }
        }

        // POST api/registration/restaurant
        [HttpPost]
        [AllowAnonymous]
        [Route("restaurant")]
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
                var registerRestaurantUserResponseDto = ResponseDtoFactory.Create(registerRestaurantUserDto);
                var passwordSalt = new PasswordSalt();
                var createRestaurantUserManager = new CreateRestaurantUserManager();

                createRestaurantUserManager.CheckIfUserExists(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                createRestaurantUserManager.HashPassword(registerRestaurantUserResponseDto, passwordSalt);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                createRestaurantUserManager.CreateNewUser(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                createRestaurantUserManager.CreateClaims(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }

                return Ok(registerRestaurantUserResponseDto.Data);
            }
            // Catch system exceptions and custom exceptions.
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest(ControllerErrorHandler.SetGeneralError());
            }
        }
    }
}
