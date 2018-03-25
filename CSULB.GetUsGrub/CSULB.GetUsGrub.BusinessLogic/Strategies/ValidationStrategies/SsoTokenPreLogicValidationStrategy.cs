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
    public class SsoTokenPreLogicValidationStrategy
    {
        private readonly string _token;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly TokenValidator _tokenValidator;

        public SsoTokenPreLogicValidationStrategy(string token, SymmetricSecurityKey key)
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
            _tokenValidator = new TokenValidator();
        }

        public ResponseDto<bool> ExecuteStrategy()
        {
            var result = _tokenValidator.CheckIfTokenIsJsonWebToken(_token);
            if (!result.Data)
            {
                return result;
            }

            result = _tokenValidator.CheckIfSsoTokenExists(_token);
            if (result.Data)
            {
                result.Error = "Token already exists.";
                result.Data = false;
                return result;
            }


            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
