using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
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
                    return BadRequest(SsoErrorMessages.NO_TOKEN_ERROR);
                }

                var result = new SsoTokenManager(request.Headers.Authorization.Parameter).ManageToken();
                Debug.WriteLine(JsonConvert.SerializeObject(result.Error));
                if (result.Error != null)
                {
                    return BadRequest(result.Error);
                }
                Debug.WriteLine(JsonConvert.SerializeObject(result));
                var userManager = new UserManager();
                var response = userManager.CreateFirstTimeSsoUser(result.Data);
                if (response.Error != null)
                {
                    return BadRequest(result.Error);
                }

                return Ok("Success.");
            }
            catch (Exception)
            {
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}