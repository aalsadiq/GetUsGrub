using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
// TODO: @Jenn Waiting for Ahmed [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        public TokenService()
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }
        
        // TODO: @Jenn Comment this [-Jenn]
        public bool CheckIfTokenIsJsonWebToken(string token)
        {
            return _jwtTokenHandler.CanReadToken(token);
        }

        public JwtSecurityToken GetJwtSecurityToken(string token)
        {
            return _jwtTokenHandler.ReadJwtToken(token);
        }

        // TODO: @Jenn Comment this [-Jenn]
        public bool ValidateSignature(string token, TokenValidationParameters tokenValidationParameters)
        {
            //var payloadHasher = new PayloadHasher();
            //// TODO: @Jenn Unit Test this encoder [-Jenn]
            //// Verify signature is valid
            //var signature = payloadHasher.Sha256HashWithSalt(salt: secret, payload: tokenHeader + "." + tokenPayload);

            //return rawSignature == signature;
            var principal = _jwtTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securitytoken);

            return true;
        }
    }
}
