using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>ClaimsValidator</c> class.
    /// Defines rules for validating Claims.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/12/2018
    /// </para>
    /// </summary>
    public class ClaimsValidator : AbstractValidator<UserClaims>
    {
        public ClaimsValidator()
        {
            RuleFor(userClaims => userClaims.Claims)
                .NotEmpty()
                .NotNull();
        }
    }
}
