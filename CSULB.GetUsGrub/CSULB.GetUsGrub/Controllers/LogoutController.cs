using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSULB.GetUsGrub.Controllers
{
    public class LogoutController : ApiController
    {
        // POST Logout/User
        [HttpPost]
        [AllowAnonymous]
        [Route("Logout")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult LogoutUserUser(HttpRequestMessage request)
        {
            try
            {
                AuthenticationTokenManager tokenManager = new AuthenticationTokenManager();
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

                AuthenticationTokenDto tokenDto = new AuthenticationTokenDto(username, tokenString);

                var revokeTokenResponse = tokenManager.RevokeToken(tokenDto);

                if (revokeTokenResponse.Error != null)
                {
                    return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
                }

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }
    }
}
