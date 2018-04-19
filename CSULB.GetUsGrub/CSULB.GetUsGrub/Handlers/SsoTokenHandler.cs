using CSULB.GetUsGrub.BusinessLogic;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub
{
    /// <summary>
    /// The <c>SSoTokenHandler</c> class.
    /// Contains properties and methods to handle a token coming from Single-Sign On.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/11/2018
    /// </para>
    /// </summary>
    public class SsoTokenHandler : DelegatingHandler
    {
        private const string AuthorizationScheme = "Bearer";

        /// <summary>
        /// The HasToken method.
        /// Checks request header if Authorization key exists.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/11/2018
        /// </para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Boolean</returns>
        private bool HasToken(HttpRequestMessage request)
        {
            if (request.Headers.Authorization != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The TryGetToken method.
        /// Gets the token from the request header with the appropriate Authorization Scheme.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/11/2018
        /// </para>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool TryGetToken(HttpRequestMessage request, out string token)
        {
            token = null;

            // Check if the authorization scheme is valid
            if (request.Headers.Authorization.Scheme == AuthorizationScheme)
            {
                // Get the token from the header
                token = request.Headers.Authorization.Parameter;
                return true;
            }

            return false;
        }

        /// <summary>
        /// The GetSigningKey method.
        /// Gets the secret key and creates a SecurityKey from it.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </para>
        /// </summary>
        /// <returns>SymmetricSecurityKey</returns>
        private SymmetricSecurityKey GetSigningKey()
        {
            var key = ConfigurationManager.AppSettings["SsoKey"];
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return signingKey;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Check if there is a token in the request
            if (!HasToken(request))
            {
                // If token does not exist return back an unauthorized HttpResponseMessage with a 401 status code
                return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized), cancellationToken);
            }

            // Get token from request
            if (!TryGetToken(request, out var token))
            {
                // If unable to fetch token with valid Authorization scheme, then return back an unauthorized HttpResponseMessage with a 401 status code
                return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized), cancellationToken);
            }

            try
            {
                var signingKey = GetSigningKey();

                // Validate token before applying business logic
                var ssoTokenStrategy = new SsoTokenValidationStrategy(token, signingKey);
                var result = ssoTokenStrategy.ExecuteStrategy();
                if (!result.Data)
                {
                    // Store invalid token into database
                    using (var ssoGateway = new SsoGateway())
                    {
                        var getTokenResult = ssoGateway.GetInvalidSsoToken(token);
                        if (getTokenResult.Data == null)
                        {
                            var storeTokenResult = ssoGateway.StoreInvalidSsoToken(new InvalidSsoToken(token));
                        }
                    }
                    return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized), cancellationToken);
                }

                return base.SendAsync(request, cancellationToken);

            }
            catch (SecurityTokenValidationException)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.Unauthorized), cancellationToken);

            }
            catch (Exception)
            {
                return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.InternalServerError), cancellationToken);
            }
        }
    }
}