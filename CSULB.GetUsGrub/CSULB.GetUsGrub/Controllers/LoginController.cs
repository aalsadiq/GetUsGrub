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
        public HttpResponseMessage AuthenticateUser([FromBody] LoginDto loginDto)
        {
            try
            {
                // Model Binding Validation
                if (!ModelState.IsValid)
                {
                    // TODO @Ahmed change to Bad request To @Ahmed
                    // TODO @Ahmed Ask @Jenn about the Action vs ResponseMessage [-Ahmed]
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Something went very wrong",
                        Configuration.Formatters.JsonFormatter);
                }
                var loginManager = new LoginManager();
                var loginResponse = loginManager.LoginUser(loginDto);
                if (loginResponse.Error != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, loginResponse.Error,
                        Configuration.Formatters.JsonFormatter);
                }
                var authenticationTokenManager = new AuthenticationTokenManager();
                var tokenResponse = authenticationTokenManager.CreateToken(loginResponse.Data);
                if (tokenResponse.Error != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, tokenResponse.Error,
                        Configuration.Formatters.JsonFormatter);
                }
                return Request.CreateResponse(HttpStatusCode.OK, tokenResponse.Data.TokenString,
                    Configuration.Formatters.JsonFormatter);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Something went very wrong",
                    Configuration.Formatters.JsonFormatter);
            }
        }

    }
}