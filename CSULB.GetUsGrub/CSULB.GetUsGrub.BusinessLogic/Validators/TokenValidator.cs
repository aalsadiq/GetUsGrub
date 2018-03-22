using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class TokenValidator
    {
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        public TokenValidator()
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        // TODO: @Jenn May need to create an interface for this [-Jenn]
        public ResponseDto<bool> CheckIfSsoTokenExists(string token)
        {
            using (var authenticationGateway = new AuthenticationGateway())
            {
                var gatewayResult = authenticationGateway.GetSsoToken(token);
                return new ResponseDto<bool>()
                {
                    // Returns true if SsoToken exists
                    Data = gatewayResult.Data != null,
                    Error = gatewayResult.Error
                };
            }
        }

        // TODO: @Jenn Comment this [-Jenn]
        public ResponseDto<bool> CheckIfTokenIsJsonWebToken(string token)
        {
            if (!_jwtTokenHandler.CanReadToken(token))
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = "Token is not a well formed Json Web Token (JWT)."
                };
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        // TODO: @Jenn Comment this [-Jenn]
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
                    Error = "Token is not valid."
                };
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
