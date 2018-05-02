using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
using System;
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
        [EnableCors(origins: "https://fannbrian.github.io,http://localhost:8085,https://localhost:8085", headers: "*", methods: "POST")]
        public IHttpActionResult Registration(HttpRequestMessage request)
        {
            try
            {
                // request.Headers.Authorization.Parameter is the token string
                var result = new SsoTokenManager(request.Headers.Authorization.Parameter).ManageRegistrationToken();
                if (result.Error != null)
                {
                    // Send 409 Response HTTP Status Code
                    return Conflict();
                }

                var userManager = new UserManager();
                var response = userManager.CreateFirstTimeSsoUser(result.Data);
                if (response.Error != null)
                {
                    // Send 409 Response HTTP Status Code
                    return Conflict();
                }

                // Send 200 Response HTTP Status Code
                return Ok();
            }
            catch (Exception)
            {
                // Send 500 Response HTTP Status Code
                return InternalServerError();
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
        [EnableCors(origins: "https://www.getusgrub.com,http://localhost:8085,https://localhost:8085", headers: " *", methods: "GET")]
        public IHttpActionResult Login(HttpRequestMessage request)
        {
            try
            {
                var tokenManager = new SsoTokenManager(request.Headers.Authorization.Parameter);
                var payloadResult = tokenManager.IsValidPayload();

                if (!payloadResult.Data)
                {
                    // Send 401 Response HTTP Status Code
                    return Unauthorized();
                }

                var tokenResult = tokenManager.ManageLoginToken();

                if (tokenResult.Error != null)
                {
                    // Send 401 Response HTTP Status Code
                    return Unauthorized();
                }

                // Send 200 Response HTTP Status Code
                return Ok(tokenResult.Data.TokenString);
            }
            catch (Exception)
            {
                // Send 500 Response HTTP Status Code
                return InternalServerError();
            }
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        [EnableCors(origins: "https://fannbrian.github.io,http://localhost:8085,https://localhost:8085", headers: "*", methods: "POST")]
        public IHttpActionResult ResetPassword(HttpRequestMessage request)
        {
            try
            {
                var result = new SsoTokenManager(request.Headers.Authorization.Parameter).ManageResetPasswordToken();
                if (result.Error != null)
                {
                    // Send 401 Response HTTP Status Code
                    return Unauthorized();
                }

                var resetPasswordManager = new ResetPasswordManager(result.Data);
                var updateResponse = resetPasswordManager.SsoUpdatePassword();

                if (updateResponse.Error != null)
                {
                    // Send 401 Response HTTP Status Code
                    return Unauthorized();
                }

                // Send 200 Response HTTP Status Code
                return Ok();
            }
            catch (Exception)
            {
                // Send 500 Response HTTP Status Code
                return InternalServerError();
            }

        }
    }
}