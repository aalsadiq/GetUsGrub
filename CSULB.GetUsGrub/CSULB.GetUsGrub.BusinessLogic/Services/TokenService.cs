using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;

// TODO: @Jenn Waiting for Ahmed [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        public TokenService()
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        // TODO: @Jenn Comment this [-Jenn]
        public bool CheckIfTokenIsJsonWebToken(string token)
        {
            return _jwtTokenHandler.CanReadToken(token);
        }

        public JwtSecurityToken GetJwtSecurityToken(string token)
        {
            return _jwtTokenHandler.ReadJwtToken(token);
        }

        // TODO: @Jenn Comment this [-Jenn]
        public bool ValidateSignature(string token, TokenValidationParameters tokenValidationParameters)
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
        /// 
        /// GetTokenPrincipal
        /// 
        /// Gets the principals from inside the token
        /// 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <param name="validatedToken"></param>
        /// <returns>
        /// Returns the ClaimsPrincipals from the token and out a validation 
        /// </returns>
        public ClaimsPrincipal GetTokenPrincipal(AuthenticationToken authenticationToken,
            out SecurityToken validatedToken, string source)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal tokenClaimsPrincipal;

            // Creating the Validation Parameter 
            var tokenValidationParameter = GetTokenValidationParameters(source , authenticationToken);

            // Validating the Token
            try
            {
                tokenClaimsPrincipal = handler.ValidateToken(authenticationToken.TokenString, tokenValidationParameter, out validatedToken);
            }
            catch (Exception)
            {
                validatedToken = null;
                return null;
            }

            return tokenClaimsPrincipal;
        }

        /// <summary>
        /// 
        /// GetTokenClaims
        /// 
        /// This Function gets the claims from inside the token
        /// 
        /// </summary>
        /// <param name="tokenString"></param>
        /// <param name="claimType"></param>
        /// <returns>
        /// 
        /// </returns>
        public Claim GetTokenClaims(AuthenticationToken tokenString, string claimType)
        {
            SecurityToken validatedToken;
            var tokenPrincipal = GetTokenPrincipal(tokenString, out validatedToken);
            if (tokenPrincipal != null)
            {
                foreach (Claim claim in tokenPrincipal.Claims)
                {
                    if (claim.Type.Equals(claimType))
                    {
                        return claim;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// This Function is used to Validate the incoming token 
        /// </summary>
        /// <param name="authenticationTokenDto"></param>
        /// <returns>
        /// Respons with JwtSecurityToken that has been validated
        /// </returns>
        public ResponseDto<JwtSecurityToken> ValidateToken(AuthenticationTokenDto authenticationTokenDto, string source)
        {
            var authenticationTokenPreLogicValidationStrategy =
                new AuthenticationTokenPreLogicValidationStrategy(authenticationTokenDto);

            // Checking if the Dto has all the information it needs and assigning it to a AuthenticationToken
            var validateAuthenticationTokenDtoResult = authenticationTokenPreLogicValidationStrategy.ExcuteStrategy();
            if (validateAuthenticationTokenDtoResult.Error != null)
            {
                return new ResponseDto<JwtSecurityToken>
                {
                    Error = validateAuthenticationTokenDtoResult.Error
                };
            }
            var incomeingAuthenticationtoken = new AuthenticationToken(authenticationTokenDto);

            // Getting the Token from the Database
            AuthenticationToken authenticationToken;
            using (var authenticationGateway = new AuthenticationGateway())
            {
                authenticationToken = authenticationGateway.GetAuthenticationToken(incomeingAuthenticationtoken).Data;
            }

            // checking the expiration date on the Token if it matches what is in the 
            if (DateTime.Compare(authenticationTokenDto.ExpiresOn, authenticationToken.ExpiresOn) != 0)
            {
                return new ResponseDto<JwtSecurityToken>
                {
                    Error = validateAuthenticationTokenDtoResult.Error
                };
            }

            GetTokenPrincipal(authenticationToken, out var validatedToken, source);

            return new ResponseDto<JwtSecurityToken>
            {
                Data = validatedToken as JwtSecurityToken
            };
        }

        /// <summary>
        /// 
        /// GetTokenValidationParameters
        /// 
        /// Get the token's parameter to help with the validation
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="authenticationToken"></param>
        /// <returns>
        /// 
        /// </returns>
        public TokenValidationParameters GetTokenValidationParameters(string source, AuthenticationToken authenticationToken)
        {
            string Audience= null, Issuer = null;
            SecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes("Nothing"));
            // If the request 
            if (source.Equals("getusgrub"))
            {
                Audience = "https://www.GetUsGrub.com";
                Issuer = "GiftHub";
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(authenticationToken.Salt)); ;
            }
            else if (source.Equals("SSO"))
            {
                Audience = "https://www.GetUsGrub.com";
                Issuer = "SSOwebsite";
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("SSOKEY"));
            }

            return new TokenValidationParameters()
            {
                ValidAudience = Audience,
                ValidIssuer = Issuer,
                IssuerSigningKey = IssuerSigningKey,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };
        }
    }
}