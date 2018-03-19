using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub
{
    [RoutePrefix("Sso")]
    public class SsoController : ApiController
    {
        [HttpPost]
        [Route("Registration")]
        // TODO: @Jenn Update origins to reflect SSO request [-Jenn]
        //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RegisterFirstTimeSsoUser(HttpRequestMessage request)
        {
            var userManager = new UserManager();

            try
            {
                ResponseDto<UserAccountDto> tokenResult;

                // TODO: @Jenn Check for if message has a token. Where to put this? [-Jenn]
                if (request.Headers.Authorization == null)
                {
                    return BadRequest("Request does not contain a token.");
                }

                using (var tokenService = new TokenService())
                {
                   tokenResult = tokenService.ValidateToken(request.Headers.Authorization.Parameter);
                }

                if (tokenResult.Error != null)
                {
                    return BadRequest(tokenResult.Error);
                }

                var result = userManager.CreateFirstTimeSsoUser(tokenResult.Data);
                if (result.Error != null)
                {
                    return BadRequest(result.Error);
                }

                return Ok(result.Data);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong. Please try again later.");
            }
        }
    }
}