using FluentValidation;

namespace GitGrub.GetUsGrub.Models
{
    public class RegisterUserPostLogicStrategy : IValidationStrategy<RegisterUserDto>
    {
        // TODO: Strategy Pattern
        public ResponseDto<RegisterUserDto> RunValidators(RegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<RegisterUserDto>() {Data = registerUserDto};

            var userAccountValidator = new UserAccountValidator();

            var validationResult = userAccountValidator.Validate(registerUserDto.UserAccount, ruleSet: "RegistrationPostLogic");

            if (!validationResult.IsValid)
            {
                responseDto.Error = "Something went wrong. Please try again later.";
                return responseDto;
            }

            // TODO: Security Questions
            // TODO: Password Salt

            return responseDto;
        }
    }
}
