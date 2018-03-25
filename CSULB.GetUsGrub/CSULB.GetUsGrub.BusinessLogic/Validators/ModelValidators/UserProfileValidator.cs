using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserProfileValidator</c> class.
    /// Defines rules to validate a UserProfile.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserProfileValidator : AbstractValidator<UserProfile>
    {
        public UserProfileValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.DisplayName)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
