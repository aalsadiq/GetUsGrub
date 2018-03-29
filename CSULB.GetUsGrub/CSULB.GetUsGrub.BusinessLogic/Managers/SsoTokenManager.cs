using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;
using System;
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
        private readonly SsoTokenValidationStrategy _ssoTokenValidationStrategy;

        public SsoTokenManager(string token)
        {
            _token = token;
            _tokenService = new TokenService();
            _ssoTokenValidationStrategy = new SsoTokenValidationStrategy(_token, GetSigningKey());
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
            string username = "";
            string password = "";
            string roleType = "";

            // Call strategy
            var result = _ssoTokenValidationStrategy.ExecuteStrategy();
            if (!result.Data)
            {
                return new ResponseDto<UserAccountDto>()
                {
                    Error = result.Error
                };
            }

            // Stores token into database
            using (var authenticationGateway = new AuthenticationGateway())
            {
                var gatewayResult = authenticationGateway.StoreSsoToken(new SsoToken(_token));
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<UserAccountDto>()
                    {
                        Error = gatewayResult.Error
                    };
                }
            }

            var jwt = _tokenService.GetJwtSecurityToken(_token);
            var payload = jwt.Payload;

            // Check if required information is in the payload
            foreach (var keyValuePair in payload)
            {
                switch (keyValuePair.Key)
                {
                    case "username":
                        username = keyValuePair.Value.ToString();
                        break;
                    case "password":
                        password = keyValuePair.Value.ToString();
                        break;
                    case "application":
                        if (keyValuePair.Value.ToString() != "GetUsGrub")
                        {
                            return new ResponseDto<UserAccountDto>()
                            {
                                Error = "Request does not match application name."
                            };
                        }
                        break;
                    case "roleType":
                        roleType = keyValuePair.Value.ToString();
                        break;
                    default:
                        break;
                }
            }

            return new ResponseDto<UserAccountDto>()
            {
                Data = new UserAccountDto(username: username, password: password, roleType: roleType)
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
        private static SymmetricSecurityKey GetSigningKey()
        {
            const string secretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            return signingKey;
        }

        public void Dispose() {}
    }
}
