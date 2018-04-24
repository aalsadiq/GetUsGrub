using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic.Validators
{
    /// <summary>
    /// The <c>ResetPasswordDtoValidator</c> class.
    /// Defines rules to validate a ResetPasswordDto.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/22/2018
    /// </para>
    /// </summary>
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleSet("Username", () =>
            {
                RuleFor(resetPasswordDto => resetPasswordDto.Username)
                    .NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.USERNAME_FORMAT);
            });

            RuleSet("UsernameAndSecurityQuestions", () =>
            {
                RuleFor(resetPasswordDto => resetPasswordDto.Username)
                    .NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.USERNAME_FORMAT);

                RuleFor(resetPasswordDto => resetPasswordDto.SecurityQuestionDtos)
                    .NotEmpty().WithMessage(ValidationErrorMessages.SECURITY_QUESTION_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.SECURITY_QUESTION_REQUIRED);
            });

            RuleSet("UsernameAndPassword", () =>
            {
                RuleFor(resetPasswordDto => resetPasswordDto.Username)
                    .NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.USERNAME_FORMAT);

                RuleFor(resetPasswordDto => resetPasswordDto.Password)
                    .NotEmpty().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .Length(8, 64).WithMessage(ValidationErrorMessages.PASSWORD_LENGTH)
                    .Matches(RegularExpressions.STRING_CONTAINS_NO_SPACES).WithMessage(ValidationErrorMessages.PASSWORD_FORMAT);
            });
        }
    }
}
