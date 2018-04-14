using CSULB.GetUsGrub.BusinessLogic.Strategies.ValidationStrategies;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CSULB.GetUsGrub.UserAccessControl;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// AuthenticationTokenManager
    /// holds all the functions that pretains to the Authentication tokens in our system
    /// </summary>
    public class AuthenticationTokenManager : TokenService
    {
        private readonly TokenService _tokenService;
        public AuthenticationTokenManager()
        {
            _tokenService = new TokenService();
        }

        /// <summary>
        /// 
        /// CreateToken
        /// 
        /// Creates a new Authentiaction Token and saves it in the Database and return it to the user
        /// 
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>
        /// Response with the AuthenticationTokenDto
        /// </returns>
        public ResponseDto<AuthenticationTokenDto> CreateToken(LoginDto loginDto)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var authenticationToken = new AuthenticationToken();
            var salt = new SaltGenerator().GenerateSalt(128);
            
            // Creating the Header of the Token
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes(salt));
            var signingCredentials = new SigningCredentials(key, "HS256");

            authenticationToken.Key = key;

            // Assigning the Username to the Token
            authenticationToken.Username = loginDto.Username;

            // Time Stamping the Token
            var issuedOn = DateTime.UtcNow;
            authenticationToken.ExpiresOn = issuedOn.AddMinutes(15);

            // Getting the ReadClaims for the user
            var claimIdentity = new ClaimsIdentity();
            var claimPrincable = new ClaimsPrincipal();
            var claimTransformer = new ClaimsTransformer();
            claimIdentity.AddClaim(new Claim("Username:", authenticationToken.Username));
            claimPrincable.AddIdentity(claimIdentity);
            var userClaimsPrincibledto = claimTransformer.Authenticate("Read", claimPrincable);
            ClaimsIdentity subjectClaimsIdentity = userClaimsPrincibledto.Identity as ClaimsIdentity;

            // Creating the Body of the token
            var tokenDescription = new SecurityTokenDescriptor
            {
                // @TODO @Ahmed incoporate the Claims from Rachel here
                Subject = subjectClaimsIdentity,
                Audience = "https://www.GetUsGrub.com",
                IssuedAt = issuedOn,
                Expires = authenticationToken.ExpiresOn,
                Issuer = "CSULB.GetUsGrub",
                SigningCredentials = signingCredentials,

            };

            // Changing the Token to a String Form
            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);
            Console.WriteLine(tokenString);
            authenticationToken.TokenString = tokenString;

            // Storing the Token to the Database
            using (var authenticationGateway = new AuthenticationGateway())
            {
                authenticationGateway.StoreAuthenticationToken(authenticationToken);
            }

            // Assigning the Token to a Dto to return it back to the User 
            var authenticationTokenDto = new AuthenticationTokenDto(authenticationToken.Username,
                authenticationToken.ExpiresOn, authenticationToken.TokenString);

            
            // Returning the Token to the Controler
            return new ResponseDto<AuthenticationTokenDto>
            {
                Data = authenticationTokenDto
            };

        }

        /// <summary>
        /// 
        /// RevokeToken
        /// 
        /// Ends the duration of the token before its Experation time
        /// 
        /// </summary>
        /// <param name="authenticationTokenDto"></param>
        /// <returns>
        /// Response with the message of session ending successfully
        /// </returns>
        public ResponseDto<AuthenticationTokenDto> RevokeToken(AuthenticationTokenDto authenticationTokenDto)
        {

            var authenticationTokenPreLogicValidationStrategy = 
                new AuthenticationTokenPreLogicValidationStrategy(authenticationTokenDto);

            // Checking if the Dto has all the information it needs
            var validateAuthenticationTokenDtoResult = authenticationTokenPreLogicValidationStrategy.ExcuteStrategy();
            if (validateAuthenticationTokenDtoResult.Error != null)
            {
                return new ResponseDto<AuthenticationTokenDto>
                {
                    Data = authenticationTokenDto,
                    Error = validateAuthenticationTokenDtoResult.Error
                };
            }

            // Changing the Experiation time on the Token
            authenticationTokenDto.ExpiresOn = DateTime.UtcNow;

            // Creating the Model to save in the DB
            var incomingAuthenticationToken = new AuthenticationToken(authenticationTokenDto.Username, authenticationTokenDto.ExpiresOn, authenticationTokenDto.TokenString);

            // Validating the Model after creation
            var authenticationTokenPostLogicValidationStrategy =
                new AuthenticationTokenPostLogicValidationStrategy(incomingAuthenticationToken);
            var validateAutenticationTokenResult = authenticationTokenPostLogicValidationStrategy.ExcuteStrategy();
            if (!validateAutenticationTokenResult)
            {
                return new ResponseDto<AuthenticationTokenDto>
                {
                    Data = authenticationTokenDto,
                    Error = "Something went wrong with : ATRT"
                };
            }


            // Updating the Token on the Database
            using (var authenticationGateway = new AuthenticationGateway())
            {
                authenticationGateway.StoreAuthenticationToken(incomingAuthenticationToken);
            }

            // Returning a message that everything went fine
            return new ResponseDto<AuthenticationTokenDto>
            {
                Data = authenticationTokenDto,
                Error = "Session Ended"
            };
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
            out SecurityToken validatedToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal tokenClaimsPrincipal;

            // Creating the Validation Parameter 
            var tokenValidationParameter = GetTokenValidationParameters(authenticationToken);

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
        public Claim GetTokenClaims(AuthenticationToken token, string claimType)
        {
            SecurityToken validatedToken;
            var tokenPrincipal = GetTokenPrincipal(token, out validatedToken);
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
        /// 
        /// Gets the parameters to help validate the token
        /// 
        /// </summary>
        /// <param name="authenticationToken"></param>
        /// <returns>
        /// TokenValidationPatameters
        /// </returns>
        public TokenValidationParameters GetTokenValidationParameters(AuthenticationToken authenticationToken)
        {
            return new TokenValidationParameters()
            {
                ValidAudience = "https://www.GetUsGrub.com",
                ValidIssuer = "CSULB.GetUsGrub",
                IssuerSigningKey = authenticationToken.Key,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };
        }

        // TODO @Ahmed Check with @Rachel how this should be modified to help both [-Ahmed]
        /// <summary>
        /// 
        /// This Method checks if the token is Authenticated or not then we extract the princible 
        /// 
        /// </summary>
        /// <param name="incomingTokenString"></param>
        /// <returns></returns>
        public ResponseDto<bool> AuthenticateToken(string incomingTokenString)
        {
            
            // Extract username from  the token
            var username = GetTokenUsername(incomingTokenString);

            // Checking if the Username is empty or null
            if (string.IsNullOrEmpty(username))
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                };
            }

            using (AuthenticationGateway gateway = new AuthenticationGateway())
            {
                // Getting the Authentication Token Associated with the username
                var gatewayResult = gateway.GetAuthenticationToken(username);

                // Checking if there was an error Generated in the 
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                        Error = gatewayResult.Error
                    };
                }

                // Checking if the string is not the same and its experation time has to be later than now
                if (gatewayResult.Data.TokenString != incomingTokenString || gatewayResult.Data.ExpiresOn.CompareTo(DateTime.Now) > 0)
                {
                    return new ResponseDto<bool>()
                    {
                        Data = false,
                    };
                }
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
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
                var username = token.Claims.First(claim => claim.Type == "UserName").Value;

                return username;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
