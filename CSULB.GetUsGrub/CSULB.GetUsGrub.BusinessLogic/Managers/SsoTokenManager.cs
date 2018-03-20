using CSULB.GetUsGrub.DataAccess;
using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

// TODO: @Jenn compare token to database. Add token to database [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class SsoTokenManager : IDisposable
    {
        private readonly SecurityKey _securityKey;
        private readonly TokenService _tokenService;

        public SsoTokenManager()
        {

            _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw=="));
            _tokenService = new TokenService();
        }

        // TODO: @Jenn Should we send back a ResponseDto? May need to encapsulate some procedures in reusable methods [-Jenn]
        public ResponseDto<UserAccountDto> ValidateToken(string token)
        {
            string username = "";
            string password = "";
            string roleType = "";
    
            if (!_tokenService.CheckIfTokenIsJsonWebToken(token))
            {
                return new ResponseDto<UserAccountDto>()
                {
                    Error = "The token is not a well formed Json Web Token."
                };
            }

            var jwt = _tokenService.GetJwtSecurityToken(token);
            var header = jwt.Header;
            var payload = jwt.Payload;
            var rawSignature = jwt.RawSignature;

            var tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = _securityKey
            };

            var result = _tokenService.ValidateSignature(token, tokenValidationParameters);
            if (!result)
            {
                return new ResponseDto<UserAccountDto>()
                {
                    Error = "Token does not have a valid signature."
                };
            }

            var json = JsonConvert.SerializeObject(jwt);
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

            using (var authenticationGateway = new AuthenticationGateway())
            {
                var gatewayResult = authenticationGateway.StoreSsoToken(new SsoToken(token));
                if (gatewayResult.Error != null)
                {
                    return new ResponseDto<UserAccountDto>()
                    {
                        Error = gatewayResult.Error
                    };
                }
            }

            return new ResponseDto<UserAccountDto>()
            {
                Data = new UserAccountDto(username: username, password: password, roleType: roleType)
            };
        }

        public void Dispose() {}
    }
}
