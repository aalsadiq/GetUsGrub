using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub
{
    // This Handler is checking if the User is Authenticated
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IEnumerable<string> _urisToSkip;

        public AuthenticationHandler()
        {
            _urisToSkip = new UniformResourceIdentifiers().UrisToSkipAuthn;
        }
        
        /// <summary>
        /// The CheckIfSkippedUri method.
        /// Checks if the Uniform Resource Identifier is in the skip Authentication list.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/17/2018
        /// </para>
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>A true or false boolean type</returns>
        public bool CheckIfSkippedUri(string uri)
        {
            return _urisToSkip.Contains(uri);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                AuthenticationTokenManager tokenManager = new AuthenticationTokenManager();
                AuthenticationToken authenticationToken;
                TokenService tokenService = new TokenService();

                // Check if the request URI absolute path should skip authentication
                if (CheckIfSkippedUri(request.RequestUri.AbsolutePath.ToLower()))
                {
                    return await base.SendAsync(request, cancellationToken);
                }

                // Send request when request has no token
                if (request.Headers.Authorization == null)
                {
                    return await base.SendAsync(request, cancellationToken);
                }
                
                // Extracting the tokenString from the Header
                var tokenString = tokenService.ExtractToken(request);

                // Checking if there is an empty or a null value to the token
                if (string.IsNullOrEmpty(tokenString))
                {
                    // This is done incase the request does not require authentication
                    return await base.SendAsync(request, cancellationToken);
                }


                // Extract username from  the token
                var username = tokenService.GetTokenUsername(tokenString);

                // Checking if the Username is empty or null
                if (string.IsNullOrEmpty(username))
                {
                    return await Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized), cancellationToken);
                }

                using (AuthenticationGateway gateway = new AuthenticationGateway())
                {
                    // Getting the Authentication Token Associated with the username
                    var gatewayResult = gateway.GetAuthenticationToken(username);

                    if (gatewayResult.Error != null || gatewayResult.Data.TokenString != tokenString || gatewayResult.Data.ExpiresOn.CompareTo(DateTime.Now) < 0)
                    {
                        return await Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized), cancellationToken);
                    }

                    authenticationToken = gatewayResult.Data;
                }

                var tokenPrincipal = tokenManager.GetTokenPrincipal(authenticationToken, out _);

                Thread.CurrentPrincipal = tokenPrincipal;

                return await base.SendAsync(request, cancellationToken);
            }
            catch (Exception)
            {
                return await Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized), cancellationToken);
            }
        }
    }
}