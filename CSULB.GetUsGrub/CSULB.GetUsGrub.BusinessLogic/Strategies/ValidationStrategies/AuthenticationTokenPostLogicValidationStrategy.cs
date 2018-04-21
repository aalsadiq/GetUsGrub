using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic.Strategies.ValidationStrategies
{
    class AuthenticationTokenPostLogicValidationStrategy
    {
        private readonly AuthenticationToken _authenticationToken;
        private readonly AuthenticationTokenValidator _authenticationTokenValidator;

        public AuthenticationTokenPostLogicValidationStrategy(AuthenticationToken authenticationToken)
        {
            _authenticationToken = authenticationToken;
            _authenticationTokenValidator = new AuthenticationTokenValidator();
        }

        public bool ExcuteStrategy()
        {
            var validationResult = _authenticationTokenValidator.Validate(_authenticationToken, ruleSet: "TokenNoSalt");

            if (!validationResult.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}
