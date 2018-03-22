using CSULB.GetUsGrub.BusinessLogic;
using System;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub
{
    //[RoutePrefix("Sso")]
    //public class SsoController : ApiController
    //{
    //    [HttpPost]
    //    [Route("Registration")]
    //    // TODO: @Jenn Update origins to reflect SSO request [-Jenn]
    //    //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
    //    public IHttpActionResult RegisterFirstTimeSsoUser(HttpRequestMessage request)
    //    {
    //        try
    //        { 
    //            // TODO: @Jenn Check for if message has a token. Where to put this? [-Jenn]
    //            if (request.Headers.Authorization == null)
    //            {
    //                return BadRequest("Request does not contain a token.");
    //            }

    //            var result = new SsoTokenManager(request.Headers.Authorization.Parameter).ValidateToken();
    //            if (result.Error != null)
    //            {
    //                return BadRequest(result.Error);
    //            }
    //            var userManager = new UserManager();
    //            var response = userManager.CreateFirstTimeSsoUser(result.Data);
    //            if (response.Error != null)
    //            {
    //                return BadRequest(response.Error);
    //            }
    //            // TODO: @Jenn Should I return a string? [-Jenn]
    //            return Ok("Success.");
    //        }
    //        catch (Exception)
    //        {
    //            return BadRequest("Something went wrong. Please try again later.");
    //        }
    //    }
    //}
}