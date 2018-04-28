using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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
        [ActionName("Registration")]
        // TODO: @Jenn Update origins to reflect SSO request when demoing [-Jenn]
        [EnableCors(origins: "https://fannbrian.github.io", headers: "*", methods: "POST")]
        public IHttpActionResult Registration(HttpRequestMessage request)
        {
            try
            {
                // request.Headers.Authorization.Parameter is the token string
                var result = new SsoTokenManager(request.Headers.Authorization.Parameter).ManageRegistrationToken();
                if (result.Error != null)
                {
                    return BadRequest(result.Error);
                }

                var userManager = new UserManager();
                var response = userManager.CreateFirstTimeSsoUser(result.Data);
                if (response.Error != null)
                {
                    return BadRequest(result.Error);
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }

        /// <summary>
        /// The <c>Sso Login</c> method.
        /// Endpoint for logging in with Sso credentials.
        /// <para>
        /// @author: Brian Fann
        /// @updated: 04/24/2018
        /// </para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Login")]
        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "GET")]
        public IHttpActionResult Login(HttpRequestMessage request)
        {
            try
            {
                var tokenManager = new SsoTokenManager(request.Headers.Authorization.Parameter);
                var payloadResult = tokenManager.IsValidPayload();

                if (!payloadResult.Data)
                {
                    return BadRequest(payloadResult.Error);
                }

                var tokenResult = tokenManager.ManageLoginToken();

                if (tokenResult.Error != null)
                {
                    return BadRequest(tokenResult.Error);
                }

                return Ok(tokenResult.Data.TokenString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return BadRequest(GeneralErrorMessages.GENERAL_ERROR);
            }
        }
    }
}