using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using System.IdentityModel.Tokens.Jwt;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenManager</c> class.
    /// Contains all methods for performing business logic for tokens from a SSO.
    /// <para>
    /// @author: Jennifer Nguyen, Brian Fann
    /// @updated: 04/23/2018
    /// </para>
    /// </summary>
    public class SsoTokenManager
    {
        private readonly SsoToken _ssoToken;
        private readonly TokenService _tokenService;

        public SsoTokenManager(string token)
        {
            _ssoToken = new SsoToken()
            {
                Token = token
            };
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
            // Map payload keys to SsoPayload model
            var mappingResult = MapRequestJwtPayloadToSsoJwtPayload();
            if (mappingResult.Error != null)
            {
                // Store invalid token into database
                StoreInvalidToken();

                return new ResponseDto<UserAccountDto>()
                {
                    Error = mappingResult.Error
                };
            }

            _ssoToken.SsoTokenPayloadDto = mappingResult.Data;

            // Validate token after applying business logic
            var ssoTokenRegistrationValidationStrategy = new SsoTokenRegistrationValidationStrategy(_ssoToken);
            var result = ssoTokenRegistrationValidationStrategy.ExecuteStrategy();
            if (!result.Data)
            {
                // Store invalid token into database
                StoreInvalidToken();

                return new ResponseDto<UserAccountDto>()
                {
                    Error = result.Error
                };
            }

            // Store valid token into database
            using (var ssoGateway = new SsoGateway())
            {
                var gatewayResult = ssoGateway.StoreValidSsoToken(new ValidSsoToken(_ssoToken.Token));
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
                Data = new UserAccountDto(username: _ssoToken.SsoTokenPayloadDto.Username, password: _ssoToken.SsoTokenPayloadDto.Password, roleType: _ssoToken.SsoTokenPayloadDto.RoleType)
            };
        }

        /// <summary>
        /// Validates payload of Sso token.
        /// </summary>
        /// <returns></returns>
        public ResponseDto<bool> IsValidPayload()
        {
            var result = MapRequestJwtPayloadToSsoJwtPayload();
            if (result.Error != null)
            {
                StoreInvalidToken();
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = result.Error
                };
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }

        /// <summary>
        /// Store an invalid token from Sso
        /// <para>
        /// @author: Jennifer Nguyen, Brian Fann
        /// @updated: 4/23/2018
        /// </para>
        /// </summary>
        /// <returns></returns>
        private ResponseDto<bool> StoreInvalidToken()
        {
            using (var ssoGateway = new SsoGateway())
            {
                var getTokenResult = ssoGateway.GetInvalidSsoToken(_ssoToken.Token);
                if (getTokenResult.Data == null)
                {
                    var storeTokenResult = ssoGateway.StoreInvalidSsoToken(new InvalidSsoToken(_ssoToken.Token));
                    return storeTokenResult;
                }
                return new ResponseDto<bool>
                {
                    Data = false
                };
            }
        }

        /// <summary>
        /// Stores a valid token from Sso.
        /// <para>
        /// @author: Jennifer Nguyen, Brian Fann
        /// @updated: 4/23/2018
        /// </para>
        /// </summary>
        /// <returns></returns>
        private ResponseDto<bool> StoreValidToken()
        {
            using (var ssoGateway = new SsoGateway())
            {
                var gatewayResult = ssoGateway.StoreValidSsoToken(new ValidSsoToken(_ssoToken.Token));
                return gatewayResult;
            }
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
        public ResponseDto<SsoTokenPayloadDto> MapRequestJwtPayloadToSsoJwtPayload()
        {
            // Convert string token to Json Web Security Token (JwtSecurityToken)
            var jwt = _tokenService.GetJwtSecurityToken(_ssoToken.Token);
            // Extract payload from JwtSecurityToken
            var payload = jwt.Payload;
            var ssoTokenPayloadDto = new SsoTokenPayloadDto();
            
            // Check if required information is in the payload
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
