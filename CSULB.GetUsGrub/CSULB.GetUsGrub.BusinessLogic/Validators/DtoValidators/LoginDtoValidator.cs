using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserAccountValidator</c> class.
    /// Defines rules to validate a UserAccountDto.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    { 
        public LoginDtoValidator()
        {
            RuleSet("UsernameAndPassword", () =>
            {
                RuleFor(loginDto => loginDto.Username)
                    .NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.USERNAME_FORMAT);

                RuleFor(loginDto => loginDto.Password)
                    .NotEmpty().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .Length(8, 64).WithMessage(ValidationErrorMessages.PASSWORD_LENGTH)
                    .Matches(RegularExpressions.STRING_CONTAINS_NO_SPACES).WithMessage(ValidationErrorMessages.PASSWORD_FORMAT);
            });
        }
    }
}
