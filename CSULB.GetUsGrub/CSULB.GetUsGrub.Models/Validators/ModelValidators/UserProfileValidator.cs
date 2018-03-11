using FluentValidation;

namespace CSULB.GetUsGrub.Models
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
            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .NotNull();
        }
    }
}
