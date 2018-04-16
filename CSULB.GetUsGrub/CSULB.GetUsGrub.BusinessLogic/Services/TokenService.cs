using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>TokenService</c> class.
    /// Contains methods that pertains to a token.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        public TokenService()
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// The GetJwtSecurityToken method.
        /// Converts a string token to a JwtSecurityToken.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtSecurityToken GetJwtSecurityToken(string token)
        {
            return _jwtTokenHandler.ReadJwtToken(token);
        }

        /// <summary>
        /// Checking if ther Token String has a Username Claim or not 
        /// </summary>
        /// <param name="incomingTokenString"></param>
        /// <returns>
        /// If there is A Username claim it returns the value of it 
        /// Else it returns null
        /// </returns>
        public string GetTokenUsername(string incomingTokenString)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(incomingTokenString);
                var username = token.Claims.First(claim => claim.Type == "Username").Value;

                return username;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// This method returns the token from the request header if there is one
        /// else it returns a null
        /// </summary>
        public string ExtractToken(HttpRequestMessage incomingRequest)
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