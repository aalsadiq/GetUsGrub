using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.IdentityModel.Tokens.Jwt;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenManager</c> class.
    /// Contains all methods for performing business logic for tokens from a SSO.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/18/2018
    /// </para>
    /// </summary>
    public class SsoTokenManager
    {
        private readonly string _token;
        private readonly TokenService _tokenService;

        public SsoTokenManager(string token)
        {
            _token = token;
            _tokenService = new TokenService();
        }

        /// <summary>
        /// The ManageToken method.
        /// Applies business logic to a token coming from the Single Sign On client for registration.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/22/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto with a UserAccountDto</returns>
        public ResponseDto<UserAccountDto> ManageRegistrationToken()
        {
            
            // Instantiating SSO token model to store to database
            var ssoToken = new SsoToken()
            {
                Token = _token
            };

            // Convert string token to Json Web Security Token (JwtSecurityToken)
            var jwt = _tokenService.GetJwtSecurityToken(_token);
            // Extract payload from JwtSecurityToken
            var payload = jwt.Payload;

            // Map payload keys to SsoPayload model
            var mappingResult = MapRequestJwtPayloadToSsoJwtPayload(payload);
            if (mappingResult.Error != null)
            {
                // Store invalid token into database
                using (var ssoGateway = new SsoGateway())
                {
                    var getTokenResult = ssoGateway.GetInvalidSsoToken(ssoToken.Token);
                    if (getTokenResult.Data == null)
                    {
                        var storeTokenResult = ssoGateway.StoreInvalidSsoToken(new InvalidSsoToken(ssoToken.Token));
                    }
                }

                return new ResponseDto<UserAccountDto>()
                {
                    Error = mappingResult.Error
                };
            }

            ssoToken.SsoTokenPayloadDto = mappingResult.Data;

            // Validate token after applying business logic
            var ssoTokenRegistrationValidationStrategy = new SsoTokenRegistrationValidationStrategy(ssoToken);
            var result = ssoTokenRegistrationValidationStrategy.ExecuteStrategy();
            if (!result.Data)
            {
                // Store invalid token into database
                using (var ssoGateway = new SsoGateway())
                {
                    var getTokenResult = ssoGateway.GetInvalidSsoToken(ssoToken.Token);
                    if (getTokenResult.Data == null)
                    {
                        var storeTokenResult = ssoGateway.StoreInvalidSsoToken(new InvalidSsoToken(ssoToken.Token));
                    }
                }

                return new ResponseDto<UserAccountDto>()
                {
                    Error = result.Error
                };
            }

            // Store valid token into database
            using (var ssoGateway = new SsoGateway())
            {
                var gatewayResult = ssoGateway.StoreValidSsoToken(new ValidSsoToken(ssoToken.Token));
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<UserAccountDto>()
                    {
                        Error = gatewayResult.Error
                    };
                }
            }

            // Send back a new UserAccountDto
            return new ResponseDto<UserAccountDto>()
            {
                Data = new UserAccountDto(username: ssoToken.SsoTokenPayloadDto.Username, password: ssoToken.SsoTokenPayloadDto.Password, roleType: ssoToken.SsoTokenPayloadDto.RoleType)
            };
        }

        /// <summary>
        /// The MapRequestJwtPayloadToSsoJwtPayload method.
        /// Maps the information inside a SSO JWT payload to the SsoJwtPayload model.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>ResponseDto with a SsoTokenPayload</returns>
        public ResponseDto<SsoTokenPayloadDto> MapRequestJwtPayloadToSsoJwtPayload(JwtPayload payload)
        {
            var ssoTokenPayloadDto = new SsoTokenPayloadDto();
            
            // Mapping required information in the payload to a data transfer object
            foreach (var keyValuePair in payload)
            {
                switch (keyValuePair.Key)
                {
                    case SsoTokenPayloadKeys.USERNAME:
                        ssoTokenPayloadDto.Username = keyValuePair.Value.ToString();
                        break;
                    case SsoTokenPayloadKeys.PASSWORD:
                        ssoTokenPayloadDto.Password = keyValuePair.Value.ToString();
                        break;
                    case SsoTokenPayloadKeys.ROLE_TYPE:
                        ssoTokenPayloadDto.RoleType = keyValuePair.Value.ToString().ToLower();
                        break;
                    case SsoTokenPayloadKeys.APPLICATION:
                        ssoTokenPayloadDto.Application = keyValuePair.Value.ToString().ToLower();
                        break;
                    case SsoTokenPayloadKeys.IAT:
                        ssoTokenPayloadDto.IssuedAt = keyValuePair.Value.ToString();
                        break;
                    default:
                        // If there are extra keys in payload, then the token is invalid
                        return new ResponseDto<SsoTokenPayloadDto>()
                        {
                            Data = null,
                            Error = SsoErrorMessages.INVALID_TOKEN_PAYLOAD
                        };
                }
            }

            return new ResponseDto<SsoTokenPayloadDto>()
            {
                Data = ssoTokenPayloadDto
            };
        }
    }
}
