using CSULB.GetUsGrub.BusinessLogic;
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
            try
            {
                // TODO: @Jenn Check for if message has a token. Where to put this? [-Jenn]
                if (request.Headers.Authorization == null)
                {
                    return BadRequest("Request does not contain a token.");
                }

                var result = new SsoTokenManager().ValidateToken(request.Headers.Authorization.Parameter);

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