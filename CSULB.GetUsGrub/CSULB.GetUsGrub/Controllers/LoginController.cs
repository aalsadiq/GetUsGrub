using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Web.Http;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.Controllers
{
    
    public class LoginController : ApiController
    {
        // POST Login/User
        [HttpPost]
        // Opts authentication
        [AllowAnonymous]
        [Route("Login/")]

        [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "POST")]

        public HttpResponseMessage AuthenticatingUser([FromBody] LoginDto loginDto)
        {
            // Model Binding Validation
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Something went very wrong", Configuration.Formatters.JsonFormatter);
            }

            
            var loginManager = new LoginManager();
            var loginResponse = loginManager.LoginUser( loginDto) ;
            if (loginResponse.Error != null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, loginResponse.Error, Configuration.Formatters.JsonFormatter);
            }

            var authenticationTokenManager = new AuthenticationTokenManager();
            var creatTokenResponse = authenticationTokenManager.CreateToken(loginResponse.Data);
            if (creatTokenResponse.Error != null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, creatTokenResponse.Error, Configuration.Formatters.JsonFormatter);
            }

            return Request.CreateResponse(HttpStatusCode.OK, creatTokenResponse.Data.TokenString, Configuration.Formatters.JsonFormatter);           
        }

    }
}
