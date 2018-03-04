using FluentValidation;

namespace GitGrub.GetUsGrub.Models
{
    public class UserAccountValidator : AbstractValidator<IUserAccount>
    {
        public UserAccountValidator()
        {
            RuleSet("RegistrationPreLogic", () =>
            {
                RuleFor(x => x.Username)
                    .NotEmpty().WithMessage("Username is required.")
                    .NotNull().WithMessage("Username is required.")
                    .Matches(@"^[A-Za-z\d]+$").WithMessage("Username must not contain spaces and special characters.")
                    .NotEqual(d => d.DisplayName).WithMessage("Username must not be the same as display name.");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .NotNull().WithMessage("Password is required.")
                    .Length(8, 64).WithMessage("Password must be at least 8 characters and less than or equal to 64.")
                    .Matches(@"[^\s]*").WithMessage("Password must not contain spaces.");

                RuleFor(x => x.DisplayName)
                    .NotEmpty().WithMessage("Display name is required")
                    .NotNull().WithMessage("Display name is required");
            });

            RuleSet("RegistrationPostLogic", () =>
            {
                RuleFor(x => x.Username)
                    .NotEmpty()
                    .NotNull()
                    .Matches(@"^[A-Za-z\d]+$")
                    .NotEqual(d => d.DisplayName);

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.DisplayName)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.IsActive)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.Claims)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}