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
    /// @updated: 03/06/2018
    /// </para>
    /// </summary>
    public class CreateUserPostLogicValidationStrategy : IValidationStrategy<IRegisterUserDto>
    {
        public ResponseDto<IRegisterUserDto> RunValidators(IRegisterUserDto registerUserDto)
        {
            var responseDto = new ResponseDto<IRegisterUserDto>() {Data = registerUserDto};

            // Validate UserAccount
            var userAccountValidator = new UserAccountValidator();
            var validationResult = userAccountValidator.Validate(registerUserDto.UserAccount, ruleSet: "RegistrationPostLogic");
            // TODO: Make this part of a Validation service to check if result is valid
            if (!validationResult.IsValid)
            {
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }

            // Validate SecurityQuestions
            var securityQuestionValidator = new SecurityQuestionValidator();
            foreach (var securityQuestion in responseDto.Data.SecurityQuestions)
            {
                validationResult = securityQuestionValidator.Validate(securityQuestion, ruleSet: "RegistrationPostLogic");
                if (validationResult.IsValid) continue;
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }
            
            // Validate SecurityAnswerSalts
            var securityAnswerSaltValidator = new SecurityAnswerSaltValidator();
            foreach (var securityAnswerSalt in responseDto.Data.SecurityAnswerSalts)
            {
                validationResult = securityAnswerSaltValidator.Validate(securityAnswerSalt);
                if (validationResult.IsValid) continue;
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }

            // Validate PasswordSalt
            var passwordSaltValidator = new PasswordSaltValidator();
            validationResult = passwordSaltValidator.Validate(responseDto.Data.PasswordSalt);
            if (!validationResult.IsValid)
            {
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }

            // Validate Claims
            var claimsValidator = new ClaimsValidator();
            validationResult = claimsValidator.Validate(responseDto.Data.Claims);
            if (!validationResult.IsValid)
            {
                responseDto.Error = ErrorHandler.GetGeneralError();
                return responseDto;
            }
            return responseDto;
        }
    }
}
