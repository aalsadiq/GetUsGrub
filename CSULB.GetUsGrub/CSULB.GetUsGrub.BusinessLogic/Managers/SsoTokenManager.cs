using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

// TODO: @Jenn compare token to database. Add token to database [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class SsoTokenManager : IDisposable
    {
        private readonly string _token;
        private readonly TokenService _tokenService;
        private readonly SsoTokenPreLogicValidationStrategy _preLogicValidationStrategy;

        public SsoTokenManager(string token)
        {
            _token = token;
            _tokenService = new TokenService();
            _preLogicValidationStrategy = new SsoTokenPreLogicValidationStrategy(_token, GetSigningKey());
        }
       
        // TODO: @Jenn Should we send back a ResponseDto? May need to encapsulate some procedures in reusable methods [-Jenn]
        public ResponseDto<UserAccountDto> ValidateToken()
        {
            string username = "";
            string password = "";
            string roleType = "";

            // Call strategy
            var result = _preLogicValidationStrategy.ExecuteStrategy();
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

        private static SymmetricSecurityKey GetSigningKey()
        {
            const string secretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            return signingKey;
        }

        public void Dispose() {}
    }
}
