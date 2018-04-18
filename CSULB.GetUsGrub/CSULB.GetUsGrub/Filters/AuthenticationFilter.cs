using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;
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

            if (!IsUserAuthorized(filterContext))
            {
                ShowAuthenticationError(filterContext);
                return;
            }
            base.OnAuthorization(filterContext);
        }

        // Message shown if there is an error
        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            var responseDTO = new ResponseDto<HttpResponseMessage>
            {
                Error =  "Code : 401"+ "Message : Unable to access, Please login again"
            };
            filterContext.Response =
                filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, responseDTO);
        }

        // Checking if the user is Authenticated and Authorized to make the requests
        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            //fetch authorization token from header
            var authHeader = FetchFromHeader(actionContext);


            if (authHeader != null)
            {
                var authenticationTokenManager = new AuthenticationTokenManager();
                AuthenticationToken userPayloadToken = null;

                if (userPayloadToken != null)
                {
                    //@TODO: I need to finish the filter and the Controler
                    
                }

            }
            return false;
        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;
            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null)
            {
                requestToken = authRequest.Parameter;
            }
            return requestToken;
        }
    }
}