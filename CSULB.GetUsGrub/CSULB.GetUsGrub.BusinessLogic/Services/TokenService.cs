using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
// TODO: @Jenn compare token to database. Add token to database [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class TokenService : IDisposable
    {
        private readonly string _ssoSecret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        // TODO: @Jenn Should we send back a ResponseDto? May need to encapsulate some procedures in reusable methods [-Jenn]
        public ResponseDto<UserAccountDto> ValidateToken(string token)
        {
            string username = "";
            string password = "";
            string roleType = "";
    
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            if (jwtTokenHandler.CanReadToken(token) == false)
            {
                return new ResponseDto<UserAccountDto>()
                {
                    Error = "The token is not a well formed Json Web Token."
                };
            }
            var jwt = jwtTokenHandler.ReadJwtToken(token);
            var header = jwt.Header;
            var payload = jwt.Payload;
            var rawSignature = jwt.RawSignature;

            var result = ValidateSignature(_ssoSecret, Base64UrlEncoder.Encode(header.ToString()), Base64UrlEncoder.Encode(payload.ToString()), rawSignature);

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

            return new ResponseDto<UserAccountDto>()
            {
                Data = new UserAccountDto(username: username, password: password, roleType: roleType)
            };
        }

        // TODO: @Jenn Comment this [-Jenn]
        public bool ValidateSignature(string secret, string tokenHeader, string tokenPayload, string rawSignature)
        {
            var payloadHasher = new PayloadHasher();
            // TODO: @Jenn Unit Test this encoder [-Jenn]
            // Verify signature is valid
            var signature = payloadHasher.Sha256HashWithSalt(salt: secret, payload: tokenHeader + "." + tokenPayload);

            return rawSignature == signature;
        }

        public void Dispose() {}
    }
}
