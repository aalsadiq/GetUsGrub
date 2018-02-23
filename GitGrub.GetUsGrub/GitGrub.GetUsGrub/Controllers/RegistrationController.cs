using GitGrub.GetUsGrub.Models;
using System;
using System.Web.Http;

namespace GitGrub.GetUsGrub
{
    [RoutePrefix("Registration")]
    public class RegistrationController : ApiController
    { 
        // POST Registration/user
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
                var registerUserResponseDto = new ResponseDto<RegisterUserDto>(registerUserDto);
                var registerUserPreLogicStrategy = new RegisterUserPreLogicStrategy();
                var createUserManager = new CreateUserManager();

                registerUserPreLogicStrategy.RunValidations(registerUserResponseDto);
                if (registerUserResponseDto.ValidationErrors != null)
                {
                    return BadRequest(registerUserResponseDto.ValidationErrors);
                }
                createUserManager.CheckUserDoesNotExist(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }
                createUserManager.HashPassword(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }
                createUserManager.CreateClaims(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }
                createUserManager.SetAccountIsActive(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }
                createUserManager.CreateNewUser(registerUserResponseDto);
                if (registerUserResponseDto.Error != null)
                {
                    return BadRequest(registerUserResponseDto.Error.Message);
                }

                // Change this to show a message with the username
                return Ok(registerUserResponseDto.Data);

            }
            // Catch custom exceptions
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            // Catch system exceptions
            catch (Exception ex)
            {
                //return BadRequest(ControllerErrorHandler.SetGeneralError());
                return BadRequest(ex.Message);
            }
        }

        // POST Registration/restaurant
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
                var registerRestaurantUserResponseDto = new ResponseDto<RegisterRestaurantUserDto>(registerRestaurantUserDto);
                var createRestaurantUserManager = new CreateRestaurantUserManager();

                createRestaurantUserManager.CheckUserDoesNotExist(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                createRestaurantUserManager.HashPassword(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                createRestaurantUserManager.CreateClaims(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                createRestaurantUserManager.SetAccountIsActive(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                createRestaurantUserManager.CreateNewUser(registerRestaurantUserResponseDto);
                if (registerRestaurantUserResponseDto.Error != null)
                {
                    return BadRequest(registerRestaurantUserResponseDto.Error.Message);
                }
                // Change this to show a message with the username
                return Ok(registerRestaurantUserResponseDto.Data);
            }
            // Catch custom exceptions
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            // Catch system exceptions
            catch (Exception)
            {
                return BadRequest(ControllerErrorHandler.SetGeneralError());
            }
        }
    }
}
