using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub.Controllers
{
    public class LogoutController : ApiController
    {
        // POST Logout/User
        [HttpPost]
        [AllowAnonymous]
        [Route("api/v1/Logout")]
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
