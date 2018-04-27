

using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    class SsoTokenRestPasswordValidationStrategy
    {
        private readonly SsoToken _ssoToken;

        public SsoTokenRestPasswordValidationStrategy(SsoToken ssoToken)
        {
            _ssoToken = ssoToken;
        }

        public ResponseDto<bool> ExecuteStrategy()
        {
            var validationWrapper = new ValidationWrapper<SsoTokenPayloadDto>(data: _ssoToken.SsoTokenPayloadDto, ruleSet: "SsoResetPassword", validator: new SsoTokenPayloadDtoValidator());
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
