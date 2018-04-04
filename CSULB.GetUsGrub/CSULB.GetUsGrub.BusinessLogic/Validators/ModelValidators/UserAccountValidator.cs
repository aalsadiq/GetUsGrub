using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserAccountValidator</c> class.
    /// Defines rules to validate a UserAccount.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserAccountValidator : AbstractValidator<UserAccount>
    {
        public UserAccountValidator()
        {
            RuleSet("CreateUser", () =>
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

                RuleFor(x => x.IsFirstTimeUser)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.RoleType)
                    .NotEmpty()
                    .NotNull()
                    .Matches(@"^((public)|(private))$");
            });

            RuleSet("SsoRegistration", () =>
            {
                RuleFor(x => x.Username)
                    .NotEmpty()
                    .NotNull()
                    .Matches(@"^[A-Za-z\d]+$");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.IsFirstTimeUser)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.RoleType)
                    .NotEmpty()
                    .NotNull()
                    .Matches(@"^((public)|(private))$");
            });
        }
    }
}
