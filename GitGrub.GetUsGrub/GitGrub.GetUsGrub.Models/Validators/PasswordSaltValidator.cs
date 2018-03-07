using FluentValidation;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>PasswordSaltValidator</c> class.
    /// Defines rules for validating a PasswordSalt.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
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
