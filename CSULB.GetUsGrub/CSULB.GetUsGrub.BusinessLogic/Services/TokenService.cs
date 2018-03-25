using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

        // TODO: @Everyone This is commented section is moved to the TokenValidator class. Please remove. [-Jenn]
        /*public bool ValidateSignature(string token, TokenValidationParameters tokenValidationParameters)
        {
            //var payloadHasher = new PayloadHasher();
            //// TODO: @Jenn Unit Test this encoder [-Jenn]
            //// Verify signature is valid
            //var signature = payloadHasher.Sha256HashWithSalt(salt: secret, payload: tokenHeader + "." + tokenPayload);

            //return rawSignature == signature;
            var principal = _jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securitytoken);

            return true;
        }

        /// <summary>
        /// This Function is used to Validate the incoming token 
        /// </summary>
        /// <param name="authenticationTokenDto"></param>
        /// <returns>
        /// Respons with JwtSecurityToken that has been validated
        /// </returns>
        public bool ValidateToken(string tokenString, TokenValidationParameters validationParameters)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            // Validating the Token
            try
            {
                jwtTokenHandler.ValidateToken(tokenString, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        */

    }
}