using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    // TODO: @Ahmed Please fix this error [-Jenn]
    class LoginPostLogicValidation
    {
        private readonly UserAuthenticationModel _userAuthenticationModel;
        private readonly UserAuthenticationModelValidator _userAuthenticationModelValidator;

        public LoginPostLogicValidation(UserAuthenticationModel userAuthenticationModel)
        {
            _userAuthenticationModel = userAuthenticationModel;
            _userAuthenticationModelValidator = new UserAuthenticationModelValidator();
        }

        public bool ExcuteStrategy()
        {
            var validationResult = _userAuthenticationModelValidator
                .Validate(_userAuthenticationModel, ruleSet: "UsernameAndPassword");

            if (!validationResult.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}
