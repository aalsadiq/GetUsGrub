using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CSULB.GetUsGrub
{
    public class AuthenticationFilter :  AuthorizationFilterAttribute
    {
        private readonly bool _isActive = true;

        public AuthenticationFilter() { }

        public AuthenticationFilter(bool isActive)
        {
            _isActive = isActive;
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            // If not active, then skip this authentication filter
            if (!_isActive) return;
        }

        /// <summary>
        /// 
        /// This method would throw an un authorized message if any expeption is thrown
        /// 
        /// </summary>
        /// <returns>
        /// Task with the response of 401 that the user is unauthenticated
        /// </returns>
        private HttpResponseMessage UserNotAuthenticated()
        {
            // Setting the message code to be a 401 
            var response = new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized };

            return response;
        }
    }
}