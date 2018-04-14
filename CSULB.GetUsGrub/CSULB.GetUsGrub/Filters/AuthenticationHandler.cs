using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub
{
    // This Handler is checking if the User is Authenticated
    public class AuthenticationHandler : DelegatingHandler
    {
        private AuthenticationTokenManager authenticationTokenManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Extracting the tokenString from the Header
                var tokenString = ExtractToken(request);
                if (string.IsNullOrEmpty(tokenString))
                {
                    return UserNotAuthenticated();
                }

                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception)
            {
                return UserNotAuthenticated();
            }
        }

       /// <summary>
       /// 
       /// This method would throw an un authorized message if any expeption is thrown
       /// 
       /// </summary>
       /// <returns>
       /// Task with the response of 401 that the user is unauthenticated
       /// </returns>
        private Task<HttpResponseMessage> UserNotAuthenticated()
        {
            var task = new TaskCompletionSource<HttpResponseMessage>();
            
            // Setting the message code to be a 401 
            var response = new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized };

            // Set response of task
            task.SetResult(response);
            
            return task.Task;
        }

        /// <summary>
        /// This method returns the token from the request header if there is one
        /// else it returns a null
        /// </summary>
        private string ExtractToken(HttpRequestMessage incomingRequest)
        {
            var requestHeaders = incomingRequest.Headers;
            string token = requestHeaders.Authorization.Parameter;

            // Cheacking if the Autherization Header is of type Bearer and that there is info in it before returning it
            if (requestHeaders.Authorization.Scheme == "Bearer" && !string.IsNullOrEmpty(token))
            {
                return token;
            }

            return null;
        }
    }
}