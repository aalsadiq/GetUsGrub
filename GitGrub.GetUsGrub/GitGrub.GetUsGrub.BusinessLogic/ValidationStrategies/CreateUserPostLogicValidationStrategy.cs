using FluentValidation;
using GitGrub.GetUsGrub.Models;
using GitGrub.GetUsGrub.Models.Validators;

namespace GitGrub.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>RegisterUserPostLogicValidationStrategy</c> class.
    /// Defines a strategy for validating models after processing business logic for registering users.
    /// Returns a general error if failure occurs.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public class CreateUserPostLogicValidationStrategy : IValidationStrategy<IRegisterUserDto>
    {
        // TODO: Strategy Pattern
        public ResponseDto<IRegisterUserDto> RunValidators(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>() {Data = registerUserDto};

            var userAccountValidator = new UserAccountValidator();

            // Validate UserAccount
            var validationResult = userAccountValidator.Validate(registerUserDto.UserAccount, ruleSet: "RegistrationPostLogic");

            if (!validationResult.IsValid)
            {
                responseDto.Error = ErrorHandler.GetGeneralError();
            }

            var securityQuestionValidator = new SecurityQuestionValidator();
            // Validate SecurityQuestions
            foreach (var securityQuestion in responseDto.Data.SecurityQuestions)
            {
                validationResult = securityQuestionValidator.Validate(securityQuestion, ruleSet: "RegistrationPostLogic");
                if (validationResult.IsValid) continue;
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }

            // TODO: Security Answers
            // TODO: PasswordSalt
            // TODO: Claims

            return responseDto;
        }
    }
}
