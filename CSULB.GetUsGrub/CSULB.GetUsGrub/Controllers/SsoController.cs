using CSULB.GetUsGrub.BusinessLogic;
using System;
using System.Net.Http;
using System.Web.Http;

namespace CSULB.GetUsGrub
{
    /// <summary>
    /// The <c>SsoController</c> class.
    /// SSO (Single Sign-On) controller will handle routes that deal wtih registering and authenticating a user from SSO.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    [RoutePrefix("Sso")]
    public class SsoController : ApiController
    {
        /// <summary>
        /// The <c>RegisterFirstTimeSsoUser</c> method.
        /// Endpoint for registering a first time SSO user.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Registration")]
        // TODO: @Jenn Update origins to reflect SSO request when demoing [-Jenn]
        //[EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]
        public IHttpActionResult RegisterFirstTimeSsoUser(HttpRequestMessage request)
        {
            try
            { 
                // Check that request header has a token
                if (request.Headers.Authorization == null)
                {
                    return BadRequest("Request does not contain a token.");
                }

                var result = new SsoTokenManager(request.Headers.Authorization.Parameter).ManageToken();
                if (result.Error != null)
                {
                    return BadRequest(result.Error);
                }
                var userManager = new UserManager();
                var response = userManager.CreateFirstTimeSsoUser(result.Data);
                if (response.Error != null)
                {
                    return BadRequest(response.Error);
                }

                return Ok("Success.");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong. Please try again later.");
            }
        }
    }
}