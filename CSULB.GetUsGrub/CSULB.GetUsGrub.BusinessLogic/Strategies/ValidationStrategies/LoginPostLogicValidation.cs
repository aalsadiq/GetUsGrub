using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    class LoginPostLogicValidation
    {
        private readonly UserAuthenticationDto _userAuthenticationDto;
        private readonly UserAuthenticationDtoValidator _userAuthenticationDtoValidator;

        public LoginPostLogicValidation(UserAuthenticationDto userAuthenticationDto)
        {
            _userAuthenticationDto = userAuthenticationDto;
            _userAuthenticationDtoValidator = new UserAuthenticationDtoValidator();
        }

        public ResponseDto<bool> ExcuteStrategy()
        {
            var validationWrapper = new ValidationWrapper<UserAuthenticationDto>(_userAuthenticationDto, "UsernameAndPassword", _userAuthenticationDtoValidator);
            var validationResult = validationWrapper.ExecuteValidator();

            if (!validationResult.Data)
            {
                return new ResponseDto<bool>()
                {
                    Data = false,
                    Error = GeneralErrorMessages.GENERAL_ERROR
                };
            }

            return new ResponseDto<bool>()
            {
                Data = true
            };
        }
    }
}
