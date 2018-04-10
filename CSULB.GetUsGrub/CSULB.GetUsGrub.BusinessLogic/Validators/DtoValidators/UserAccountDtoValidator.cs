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
    public class UserAccountDtoValidator : AbstractValidator<UserAccountDto>
    {
        public UserAccountDtoValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(userAccountDto => userAccountDto.Username)
                    .NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.USERNAME_FORMAT);

                RuleFor(userAccountDto => userAccountDto.Password)
                    .NotEmpty().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .Length(8, 64).WithMessage(ValidationErrorMessages.PASSWORD_LENGTH)
                    .Matches(RegularExpressions.STRING_CONTAINS_NO_SPACES).WithMessage(ValidationErrorMessages.PASSWORD_FORMAT);
            });

            // TODO: @Jenn May not need this [-Jenn]
            RuleSet("SsoRegistration", () =>
            {
                RuleFor(userAccountDto => userAccountDto.Username)
                    .NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.USERNAME_REQUIRED)
                    .Matches(RegularExpressions.USERNAME_FORMAT).WithMessage(ValidationErrorMessages.USERNAME_FORMAT);

                RuleFor(userAccountDto => userAccountDto.Password)
                    .NotEmpty().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.PASSWORD_REQUIRED)
                    .Length(8, 64).WithMessage(ValidationErrorMessages.PASSWORD_LENGTH)
                    .Matches(RegularExpressions.STRING_CONTAINS_NO_SPACES).WithMessage(ValidationErrorMessages.PASSWORD_FORMAT);

                RuleFor(userAccountDto => userAccountDto.RoleType)
                    .NotEmpty()
                    .NotNull()
                    .Matches(@"^((public)|(private))$").WithMessage("Must be 'public' or 'private' role type.");
            });

            RuleSet("Username", () =>
            {
                RuleFor(userAccount => userAccount.Username)
                    .NotEmpty().WithMessage("Username is required.")
                    .NotNull().WithMessage("Username is required.")
                    .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must not contain spaces and special characters.");
            });
        }
    }
}
