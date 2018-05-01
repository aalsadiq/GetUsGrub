using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>TokenValidator</c> class.
    /// Defines rules to validate a token.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class TokenValidator
    {
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        public TokenValidator()
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// The CheckIfTokenIsJsonWebToken method.
        /// Checks if the token is a valid Json Web Token.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ResponseDto<bool> CheckIfTokenIsJsonWebToken(string token)
        {
            if (!_jwtTokenHandler.CanReadToken(token))
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = ValidationErrorMessages.INVALID_TOKEN
                };
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// The CheckIfSsoTokenExists method.
        /// Checks if there is an SSO token in the database.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ResponseDto<bool> CheckIfSsoTokenExists(string token)
        {
            using (var ssoGateway = new SsoGateway())
            {
                var gatewayResult = ssoGateway.GetValidSsoToken(token);
                return new ResponseDto<bool>()
                {
                    // Returns true if SsoToken exists
                    Data = gatewayResult.Data != null,
                    Error = gatewayResult.Error
                };
            }
        }

        /// <summary>
        /// The ValidateJwt method.
        /// Checks if the Jwt has valid parameters defined for the token.
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenValidationParameters"></param>
        /// <returns></returns>
        public ResponseDto<bool> ValidateJwt(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;
                _jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = ValidationErrorMessages.INVALID_TOKEN
                };
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
