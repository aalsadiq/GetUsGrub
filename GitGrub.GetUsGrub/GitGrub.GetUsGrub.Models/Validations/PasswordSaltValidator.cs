using FluentValidation;

namespace GitGrub.GetUsGrub.Models
{
    public class PasswordSaltValidator : AbstractValidator<ISalt>
    {
        public PasswordSaltValidator()
        {
            RuleFor(x => x.Salt)
                .NotEmpty()
                .NotNull();
        }
    }
}
