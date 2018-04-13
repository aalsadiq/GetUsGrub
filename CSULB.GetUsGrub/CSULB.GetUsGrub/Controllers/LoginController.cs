using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Http;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.Controllers
{
    
    public class LoginController : ApiController
    {
        // POST Login/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Login/")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult AuthenticateUser([FromBody] LoginDto loginDto)
        {
            try
            {
                // Model Binding Validation
                if (!ModelState.IsValid)
                {
                   return BadRequest(ModelState);
                }
                var loginManager = new LoginManager();
                var loginResponse = loginManager.LoginUser(loginDto);
                if (loginResponse.Error != null)
                {
                    return BadRequest(loginResponse.Error);
                }
                var authenticationTokenManager = new AuthenticationTokenManager();
                var tokenResponse = authenticationTokenManager.CreateToken(loginResponse.Data);
                if (tokenResponse.Error != null)
                {
                    return BadRequest();
                }
                return Ok(tokenResponse.Data.TokenString);
            }
            catch (Exception)
            {
                return BadRequest("Something Went Completely Wrong!");
            }
        }
    }
}