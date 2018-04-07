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
                RuleFor(UserAccount => UserAccount.Username)
                    .NotEmpty().WithMessage("Username is required.")
                    .NotNull().WithMessage("Username is required.")
                    .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must not contain spaces and special characters.");

                RuleFor(UserAccount => UserAccount.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .NotNull().WithMessage("Password is required.")
                    .Length(8, 64).WithMessage("Password must be at least 8 characters and less than or equal to 64.")
                    .Matches(@"^[^\s]+$").WithMessage("Password must not be empty or contain spaces.");
            });

            RuleSet("SsoRegistration", () =>
            {
                RuleFor(UserAccount => UserAccount.Username)
                    .NotEmpty().WithMessage("Username is required.")
                    .NotNull().WithMessage("Username is required.")
                    .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must not contain spaces and special characters.");

                RuleFor(UserAccount => UserAccount.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .NotNull().WithMessage("Password is required.")
                    .Length(8, 64).WithMessage("Password must be at least 8 characters and less than or equal to 64.")
                    .Matches(@"^[^\s]+$").WithMessage("Password must not be empty or contain spaces.");

                RuleFor(UserAccount => UserAccount.RoleType)
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
