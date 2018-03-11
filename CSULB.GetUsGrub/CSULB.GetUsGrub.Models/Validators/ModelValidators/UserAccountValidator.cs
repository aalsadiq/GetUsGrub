using FluentValidation;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccountValidator</c> class.
    /// Defines rules to validate a UserAccount.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserAccountValidator : AbstractValidator<UserAccount>
    {
        public UserAccountValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .Matches(@"^[A-Za-z\d]+$");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.IsActive)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.FirstTimeUser)
                .NotEmpty()
                .NotNull();
        }
    }
}
