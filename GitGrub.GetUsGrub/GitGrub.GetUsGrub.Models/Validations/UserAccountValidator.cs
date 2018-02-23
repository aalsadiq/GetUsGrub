using FluentValidation;

namespace GitGrub.GetUsGrub.Models
{
    public class UserAccountValidator : AbstractValidator<RegisterUserDto>
    {
        public UserAccountValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                .Matches(@"[\s]*").WithMessage("Username must not contain spaces.")
                .NotEqual(d => d.DisplayName).WithMessage("Username must not be the same as display name.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .Length(8, 128).WithMessage("Password must be at least 8 characters and less than or equal to 120.")
                .Matches(@"[^\s]*(?=[^\s]*[a-z])(?=[^\s]*[A-Z])(?=[^\s]*[#@!$])[^\s]*")
                .WithMessage("Password needs at least one uppercase letter, one lowercase letter, and one of the following special characters (#, @, !, $).");
        }
    }
}