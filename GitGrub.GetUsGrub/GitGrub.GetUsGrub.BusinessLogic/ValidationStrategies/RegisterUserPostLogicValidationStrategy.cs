using FluentValidation;
using GitGrub.GetUsGrub.Models;

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
    public class RegisterUserPostLogicValidationStrategy : IValidationStrategy<IRegisterUserDto>
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

            // TODO: Password Salt
            // TODO: Security Questions
            // TODO: Security Answers
            // TODO: Claims

            return responseDto;
        }
    }
}
