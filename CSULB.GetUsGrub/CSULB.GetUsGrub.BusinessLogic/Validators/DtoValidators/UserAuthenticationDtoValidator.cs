using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>UserAccountValidator</c> class.
    /// Defines rules to validate a UserAuthenticationDto.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    class UserAuthenticationDtoValidator : AbstractValidator<UserAuthenticationDto>
    {
        public UserAuthenticationDtoValidator()
        {
            RuleSet("UsernameAndPassword", () =>
            {
                RuleFor(userAuthenticationDto => userAuthenticationDto.Username)
                    .NotEmpty()
                    .NotNull()
                    .Matches(RegularExpressions.USERNAME_FORMAT);

                RuleFor(userAuthenticationDto => userAuthenticationDto.Password)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
