using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub.Controllers
{

    public class LoginController : ApiController
    {
        // POST Login/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("api/v1/Login")]
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
        [Route("api/v1/RenewSession")]
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