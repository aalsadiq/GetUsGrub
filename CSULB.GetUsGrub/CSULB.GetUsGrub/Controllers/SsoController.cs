using System;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub
{
    [RoutePrefix("Sso")]
    public class SsoController : ApiController
    {
        [HttpPost]
        [Route("Registration/Individual")]
        //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RegisterFirstTimeSsoUser(HttpRequestMessage request)
        {
            try
            {
                var requestHeaders = request.Headers;
                if (requestHeaders.Contains("Bearer"))
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}