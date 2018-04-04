using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenManager</c> class.
    /// Contains all methods for performing business logic to a token from a SSO.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/18/2018
    /// </para>
    /// </summary>
    public class SsoTokenManager : IDisposable
    {
        private readonly string _token;
        private readonly TokenService _tokenService;
        private readonly SsoTokenPreLogicValidationStrategy _ssoTokenPreLogicValidationStrategy;

        public SsoTokenManager(string token)
        {
            _token = token;
            _tokenService = new TokenService();
            _ssoTokenPreLogicValidationStrategy = new SsoTokenPreLogicValidationStrategy(_token, GetSigningKey());
        }

        /// <summary>
        /// The ManageToken method.
        /// Applies business logic to a token coming from the Single Sign On client.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto with a UserAccountDto</returns>
        public ResponseDto<UserAccountDto> ManageToken()
        {
            // Validate token before applying business logic
            var result = _ssoTokenPreLogicValidationStrategy.ExecuteStrategy();
            if (!result.Data)
            {
                return new ResponseDto<UserAccountDto>()
                {
                    Error = result.Error
                };
            }

            Debug.WriteLine("Here");
            // Creating a new SsoToken model after validation
            var ssoToken = new SsoToken(_token);

            // Stores token into database
            using (var authenticationGateway = new AuthenticationGateway())
            {
                var gatewayResult = authenticationGateway.StoreSsoToken(ssoToken);
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<UserAccountDto>()
                    {
                        Error = gatewayResult.Error
                    };
                }
            }
            Debug.WriteLine("Here2");
            // Convert string token to Json Web Security Token (JwtSecurityToken)
            var jwt = _tokenService.GetJwtSecurityToken(_token);
            // Extract payload from JwtSecurityToken
            var payload = jwt.Payload;

            // Map payload keys to SsoPayload model
            var mappingResult = MapRequestJwtPayloadToSsoJwtPayload(payload);
            if (mappingResult.Error != null)
            {
                return new ResponseDto<UserAccountDto>()
                {
                    Error = mappingResult.Error
                };
            }
            Debug.WriteLine("Here3");
            ssoToken.SsoTokenPayload = mappingResult.Data;
            Debug.WriteLine("Here4");
            // Validate token after applying business logic
            var ssoTokenPostLogicValidationStrategy = new SsoTokenPostLogicValidationStrategy(ssoToken);
            result = ssoTokenPostLogicValidationStrategy.ExecuteStrategy();
            if (!result.Data)
            {
                return new ResponseDto<UserAccountDto>()
                {
                    Error = result.Error
                };
            }
            Debug.WriteLine("Here5");
            // TODO: @Jenn should I do this? [-Jenn]
            // Send back a new UserAccountDto
            return new ResponseDto<UserAccountDto>()
            {
                Data = new UserAccountDto(username: ssoToken.SsoTokenPayload.Username, password: ssoToken.SsoTokenPayload.Password, roleType: ssoToken.SsoTokenPayload.RoleType)
            };
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
            // TODO: @Jenn Where to hide this secret key? Also will need a new secret key [-Jenn]
            const string secretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            return signingKey;
        }

        // TODO: @Jenn Comment this  [-Jenn]
        public ResponseDto<SsoTokenPayload> MapRequestJwtPayloadToSsoJwtPayload(JwtPayload payload)
        {
            var ssoTokenPayload = new SsoTokenPayload();
            
            // Check if required information is in the payload
            foreach (var keyValuePair in payload)
            {
                switch (keyValuePair.Key)
                {
                    case SsoTokenPayloadKeys.USERNAME:
                        ssoTokenPayload.Username = keyValuePair.Value.ToString();
                        break;
                    case SsoTokenPayloadKeys.PASSWORD:
                        ssoTokenPayload.Password = keyValuePair.Value.ToString();
                        break;
                    case SsoTokenPayloadKeys.ROLE_TYPE:
                        ssoTokenPayload.RoleType = keyValuePair.Value.ToString().ToLower();
                        break;
                    case SsoTokenPayloadKeys.APPLICATION:
                        ssoTokenPayload.Application = keyValuePair.Value.ToString().ToLower();
                        break;
                    case SsoTokenPayloadKeys.IAT:
                        ssoTokenPayload.Application = keyValuePair.Value.ToString();
                        break;
                    default:
                        return new ResponseDto<SsoTokenPayload>()
                        {
                            Data = null,
                            Error = SsoErrorMessages.INVALID_TOKEN_PAYLOAD
                        };
                }
            }

            return new ResponseDto<SsoTokenPayload>()
            {
                Data = ssoTokenPayload
            };
        }

        public void Dispose() {}
    }
}
