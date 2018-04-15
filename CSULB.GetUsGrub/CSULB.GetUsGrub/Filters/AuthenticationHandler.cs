using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub
{
    // This Handler is checking if the User is Authenticated
    public class AuthenticationHandler : DelegatingHandler
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected HttpResponseMessage Send(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Microsoft.IdentityModel.Tokens.SecurityToken validatedToken;
            Debug.WriteLine("Here");
            try
            {
                AuthenticationTokenManager tokenManager = new AuthenticationTokenManager();
                AuthenticationToken authenticationToken;

                // Extracting the tokenString from the Header
                var tokenString = ExtractToken(request);

                // Checking if there is an empty or a null value to the token
                if (string.IsNullOrEmpty(tokenString))
                {
                    // This is done incase the request does not require authentication
                    return Task.Run(() => SendAsync(request, cancellationToken)).Result;
                }


                // Extract username from  the token
                var username = tokenManager.GetTokenUsername(tokenString);

                // Checking if the Username is empty or null
                if (string.IsNullOrEmpty(username))
                {
                    return UserNotAuthenticated();
                }

                using (AuthenticationGateway gateway = new AuthenticationGateway())
                {
                    // Getting the Authentication Token Associated with the username
                    var gatewayResult = gateway.GetAuthenticationToken(username);

                    // Checking if there was an error Generated in the gateway if the string is not the same and its experation time has to be later than now
                    if (gatewayResult.Error != null|| gatewayResult.Data.TokenString != tokenString ||
                        gatewayResult.Data.ExpiresOn.CompareTo(DateTime.Now) > 0)
                    {
                        return UserNotAuthenticated();
                    }
   
                    authenticationToken = gatewayResult.Data;
                }

                
                var tokenPrincible = tokenManager.GetTokenPrincipal(authenticationToken, out validatedToken);

                Thread.CurrentPrincipal = tokenPrincible;

                return Task.Run(() => SendAsync(request, cancellationToken)).Result;
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
        private HttpResponseMessage UserNotAuthenticated()
        {
            // Setting the message code to be a 401 
            var response = new HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized };
            
            return response;
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