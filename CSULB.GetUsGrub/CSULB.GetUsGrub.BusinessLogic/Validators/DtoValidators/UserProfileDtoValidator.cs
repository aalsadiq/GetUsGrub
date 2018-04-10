using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserProfileDtoValidator</c> class.
    /// Defines rules to validate a UserProfileDto.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class UserProfileDtoValidator : AbstractValidator<UserProfileDto>
    {
        public UserProfileDtoValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(userProfileDto => userProfileDto.DisplayName)
                    .NotEmpty().WithMessage(ValidationErrorMessages.DISPLAY_NAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.DISPLAY_NAME_REQUIRED);
            });
        }
    }
}
