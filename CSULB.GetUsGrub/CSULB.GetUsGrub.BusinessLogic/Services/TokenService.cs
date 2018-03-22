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

        public JwtSecurityToken GetJwtSecurityToken(string token)
        {
            return _jwtTokenHandler.ReadJwtToken(token);
        }
    }
}
