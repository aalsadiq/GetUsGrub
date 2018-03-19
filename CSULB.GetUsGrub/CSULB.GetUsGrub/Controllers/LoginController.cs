using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.Controllers
{
    [RoutePrefix("Login")] //default route
    public class LoginController : ApiController
    {
        // POST Login/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
  
        public IHttpActionResult AuthenticatingUser([FromBody] LoginDto loginDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var loginManager = new LoginManager();
                var loginResponse = loginManager.LoginUser( loginDto) ;
                if (loginResponse.Error != null)
                {
                    return BadRequest(loginResponse.Error);
                }

                var authenticationTokenManager = new AuthenticationTokenManager();
                var creatTokenResponse = authenticationTokenManager.CreateToken(loginResponse.Data);



            }

            // Catch exceptions
            catch (Exception)
            {
                // HTTP 400 Status
                return BadRequest("Something went wrong. Pleast try again later.");
            }
        }

    }
}
