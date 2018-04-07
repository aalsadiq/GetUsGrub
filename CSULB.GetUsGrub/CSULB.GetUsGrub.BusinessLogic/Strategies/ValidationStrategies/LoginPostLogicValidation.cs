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

        public bool ExcuteStrategy()
        {
            var validationResult = _userAuthenticationDtoValidator
                .Validate(_userAuthenticationDto, ruleSet: "UsernameAndPassword");

            if (!validationResult.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}
