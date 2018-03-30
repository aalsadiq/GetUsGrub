using CSULB.GetUsGrub.Models;
using Microsoft.IdentityModel.Tokens;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenPreLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models before processing business logic for validating a token from Single Sign On.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class SsoTokenValidationStrategy
    {
        private readonly string _token;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly SsoTokenValidator _ssotokenValidator;
        private readonly TokenService _tokenService;

        public SsoTokenValidationStrategy(string token, SymmetricSecurityKey key)
        {
            _token = token;
            _tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };
            _ssotokenValidator = new SsoTokenValidator();
            _tokenService = new TokenService();
        }

        public ResponseDto<bool> ExecuteStrategy()
        {
            var result = _ssotokenValidator.CheckIfTokenIsJsonWebToken(_token);
            if (!result.Data)
            {
                return result;
            }

            result = _ssotokenValidator.CheckIfSsoTokenExists(_token);
            if (result.Data)
            {
                result.Error = "Token already exists.";
                result.Data = false;
                return result;
            }

            result = _ssotokenValidator.ValidateJwt(_token, _tokenValidationParameters);
            if (!result.Data)
            {
                return result;
            }

            // TODO: @Jenn Make a call to the SsoTokenValidator [-Jenn]
            foreach (var key in _tokenService.GetJwtSecurityToken(_token).Payload)
            {

            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
