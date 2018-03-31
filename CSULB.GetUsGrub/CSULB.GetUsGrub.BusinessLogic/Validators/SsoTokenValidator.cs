using CSULB.GetUsGrub.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class SsoTokenValidator : TokenValidator
    {
        public enum ValidPayloadKeys
        {
            Username,
            Password,
            Application,
            Roletype,
            Iat
        }

        public ResponseDto<bool> ValidateRequiredKeysInJwtPayload(JwtPayload jwtPayload,
            ICollection<string> payloadKeys)
        {
            // TODO: @Jenn Figure out how to map the keys to a model? [-Jenn]
            string username = "";
            string password = "";
            string roleType = "";

            // Validate list is size of enum
            if (sizeof(ValidPayloadKeys) != payloadKeys.Count)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = "Jwt payload is invalid."
                };
            }

            foreach (var keyValuePair in jwtPayload)
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
                            return new ResponseDto<bool>()
                            {
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
            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
