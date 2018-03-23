using System.IdentityModel.Tokens.Jwt;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>TokenService</c> class.
    /// Contains methods that pertains to a token.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;

        public TokenService()
        {
            _jwtTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// The GetJwtSecurityToken method.
        /// Converts a string token to a JwtSecurityToken.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtSecurityToken GetJwtSecurityToken(string token)
        {
            return _jwtTokenHandler.ReadJwtToken(token);
        }
    }
}
