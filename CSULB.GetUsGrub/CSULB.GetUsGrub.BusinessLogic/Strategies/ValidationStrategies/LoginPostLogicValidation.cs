using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    class LoginPostLogicValidation
    {
        private readonly UserAuthenticationModel _userAuthenticationModel;
        private readonly UserAuthenticationModelValidator _userAuthenticationModelValidator;

        public LoginPostLogicValidation(UserAuthenticationModel userAuthenticationModel)
        {
            _userAuthenticationModel = userAuthenticationModel;
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
