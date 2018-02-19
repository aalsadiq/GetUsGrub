using GitGrub.GetUsGrub.Interfaces;
using GitGrub.GetUsGrub.Models.DTOs;
using System;
using System.Web.Http;

namespace GitGrub.GetUsGrub.Controllers
{
    // Is this an okay route attribute for registration?
    [RoutePrefix("api/registration")]
    public class RegistrationController : ApiController
    {
        // Should I make my IUserAccountManager private?
        // You want to make your controller aware of an interface rather than the 
        // actual manager class so you can change the implementation of the 
        // manager class at any time and for how many times you want
        // without breaking the host code
        private readonly ICreateUserManager _createUserManager;

        // Constructor injection usage
        // In RegisterController constructor, implement the IUserAccountManager 
        // using dependency injection
        // We never created an object, but communicated with one layer and another with
        // the help of an interface
        public RegistrationController(ICreateUserManager createUserManager)
        {
            _createUserManager = createUserManager;
        }

        // POST api/registration/user
        [HttpPost]
        // AllowAnonymous opts out authentication for the user
        [AllowAnonymous]
        [Route("user")]
        // Possible responses/HTTP status codes for IHttpActionResult can be found in 
        // System.Web.Http.Results
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
            // Catch system exceptions and custom exceptions. May just want to return CustomExceptions to users?
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(registerUserWithSecurityDto);
        }
    }
}
