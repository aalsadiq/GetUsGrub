using GitGrub.GetUsGrub.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;
using System;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    [RoutePrefix("api/registration")]
    public class RegistrationController : ApiController
    {
        private readonly ICreateUserManager _createUserManager;

        public RegistrationController(ICreateUserManager createUserManager)
        {
            _createUserManager = createUserManager;
        }

        // POST api/registration/user
        [HttpPost]
        // AllowAnonymous opts out authentication for the user
        [AllowAnonymous]
        [Route("user")]
        public IHttpActionResult RegisterUserAccount([FromBody] RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _createUserManager.CheckIfUserExists(registerUserWithSecurityDto);
                _createUserManager.HashPassword(registerUserWithSecurityDto);

            }
            // Catch system exceptions and custom exceptions
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(registerUserWithSecurityDto);
        }

        // POST api/registration/restaurant
        [HttpPost]
        [AllowAnonymous]
        [Route("restaurant")]
        public IHttpActionResult RegisterRestaurantAccount([FromBody] RegisterUserWithSecurityDto registerUserWithSecurityDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _createUserManager.HashPassword(registerUserWithSecurityDto);
            }
            // Catch system exceptions and custom exceptions
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(registerUserWithSecurityDto);
        }
    }
}
