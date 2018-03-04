using FluentValidation;

namespace GitGrub.GetUsGrub.Models
{
    public class RegisterUserPostLogicStrategy : IValidationStrategy<IRegisterUserDto>
    {
        // TODO: Strategy Pattern
        public ResponseDto<IRegisterUserDto> RunValidators(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>();

            var userAccountValidator = new UserAccountValidator();

            var validationResult = userAccountValidator.Validate(registerUserDto, ruleSet: "RegistrationPostLogic");

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
