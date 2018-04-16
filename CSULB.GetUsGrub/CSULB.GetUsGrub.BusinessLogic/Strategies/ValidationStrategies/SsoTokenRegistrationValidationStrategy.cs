using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SsoTokenRegistrationValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for validating a token from Single Sign On.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/22/2018
    /// </para>
    /// </summary>
    public class SsoTokenRegistrationValidationStrategy
    {
        private readonly SsoToken _ssoToken;

        public SsoTokenRegistrationValidationStrategy(SsoToken ssoToken)
        {
            _ssoToken = ssoToken;
            
        }

        /// <summary>
        /// The ExecuteStrategy method.
        /// Contains the logic to validate a SsoToken.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/04/2018
        /// </para>
        /// </summary>
        /// <returns>ResponseDto</returns>
        public ResponseDto<bool> ExecuteStrategy()
        {

            var validationWrapper = new ValidationWrapper<SsoTokenPayloadDto>(data: _ssoToken.SsoTokenPayloadDto, ruleSet: "SsoRegistration", validator: new SsoTokenPayloadDtoValidator());

            var result = validationWrapper.ExecuteValidator();
            if (!result.Data)
            {
                result.Error = SsoErrorMessages.INVALID_TOKEN_PAYLOAD;
                return result;
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
