using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{

    public class LoginController : ApiController
    {
        // POST Login/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Login")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult AuthenticateUser([FromBody] LoginDto loginDto)
        {
            try
            {
                // Model Binding Validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(GeneralErrorMessages.MODEL_STATE_ERROR);
                }
                var loginManager = new LoginManager();
                var loginResponse = loginManager.LoginUser(loginDto);
                if (loginResponse.Error != null)
                {
                    return BadRequest(loginResponse.Error);
                }
                var authenticationTokenManager = new AuthenticationTokenManager();
                var tokenResponse = authenticationTokenManager.CreateToken(loginResponse.Data.Username);
                return Ok(tokenResponse.Data.TokenString);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("RenewSession")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RenewSession(HttpRequestMessage request)
        {
            try
            {
                TokenService tokenService = new TokenService();


                var tokenString = tokenService.ExtractToken(request);
                if (string.IsNullOrEmpty(tokenString))
                {
                    return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
                }

                var username = tokenService.GetTokenUsername(tokenString);
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
                }

                var authenticationTokenManager = new AuthenticationTokenManager();
                var tokenResponse = authenticationTokenManager.CreateToken(username);
                return Ok(tokenResponse.Data.TokenString);
            }
            catch (Exception)
            {
                return InternalServerError(); 
            }
        }
    }
}