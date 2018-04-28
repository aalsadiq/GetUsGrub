using CSULB.GetUsGrub.BusinessLogic.Strategies.ValidationStrategies;
using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using CSULB.GetUsGrub.UserAccessControl;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CSULB.GetUsGrub.Models.Constants.TokenPayloadConstants;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// AuthenticationTokenManager
    /// holds all the functions that pretains to the Authentication tokens in our system
    /// </summary>
    public class AuthenticationTokenManager
    {
        /// <summary>
        /// 
        /// CreateToken
        /// 
        /// Creates a new Authentiaction Token and saves it in the Database and return it to the user
        /// 
        /// </summary>
        /// <para>
        /// @author: Ahmed Sadiq, Brian Fann, Rachel Dang
        /// @updated: 4/26/18
        /// </para>
        /// <param name="loginDto"></param>
        /// <returns>
        /// Response with the AuthenticationTokenDto
        /// </returns>
        public ResponseDto<AuthenticationTokenDto> CreateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authenticationToken = new AuthenticationToken();
            var salt = new SaltGenerator().GenerateSalt(128);

            // Creating the Header of the Token
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes(salt));
            var signingCredentials = new SigningCredentials(key, "HS256");

            authenticationToken.Salt = salt;

            // Assigning the Username to the Token
            authenticationToken.Username = username;

            // Time Stamping the Token
            var issuedOn = DateTime.UtcNow;
            authenticationToken.ExpiresOn = issuedOn.AddMinutes(15);

            // Create claims identity with "Read" permisison claims
            var claimsIdentity = CreateClaimsIdentity(username).Data;            

            // Creating the Body of the token
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Audience = AuthenticationTokenConstants.AUDIENCE,
                IssuedAt = issuedOn,
                Expires = authenticationToken.ExpiresOn,
                Issuer = AuthenticationTokenConstants.ISSUER,
                SigningCredentials = signingCredentials,
            };

            // Changing the Token to a String Form
            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);
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
            authenticationTokenDto.ExpiresOn = DateTime.UtcNow; // Set this to the past

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
                    Error = GeneralErrorMessages.GENERAL_ERROR
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
                ValidAudience = AuthenticationTokenConstants.AUDIENCE,
                ValidIssuer = AuthenticationTokenConstants.ISSUER,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(authenticationToken.Salt)),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };
        }

        /// <summary>
        /// Method to check if user is a first time user
        /// 
        /// @author: Rachel Dang
        /// @updated: 04/28/18
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Bool?</returns>
        private ResponseDto<bool?> CheckIfFirstTimeUser(string username)
        {
            using (var gateway = new UserGateway())
            {
                // Call gateway to get user by username
                var user = gateway.GetUserByUsername(username);

                // If user is invalid and an error occurs, return dto with error
                if (user.Error != null)
                {
                    return new ResponseDto<bool?>
                    {
                        Data = false,
                        Error = user.Error
                    };
                }

                // Return dto with user's isFirstTimeUser value
                return new ResponseDto<bool?>
                {
                    Data = user.Data.IsFirstTimeUser
                };
            }
        }

        /// <summary>
        /// Method to call the claims transformer to create the 
        /// 
        /// @author: Rachel Dang
        /// @updated: 04/28/18
        /// </summary>
        /// <param name="username"></param>
        /// <param name="isFirstTimeUser"></param>
        /// <returns></returns>
        private ResponseDto<ClaimsIdentity> CreateClaimsIdentity(string username)
        {
            // Check if user is a first time user from the SSO       
            var isFirstTimeUser = CheckIfFirstTimeUser(username);

            // If user is invalid and an error occurs, return dto with error
            if (isFirstTimeUser.Error != null)
            {
                return new ResponseDto<ClaimsIdentity>
                {
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            // Call the Claims Transformer manager to create the claims identity
            var transformer = new ClaimsTransformer();
            if (isFirstTimeUser.Data == true)
            {
                new ResponseDto<ClaimsIdentity>
                {
                    Data = transformer.CreateSsoClaimsIdentity(username)
                };
            }
            return new ResponseDto<ClaimsIdentity>
            {
                Data = transformer.CreateAuthenticationClaimsIdentity(username)
            };
        }
    }
}