using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>SecurityAnswerSaltValidator</c> class.
    /// Defines rules for validating a SecurityAnswer.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class SecurityAnswerSaltValidator : AbstractValidator<SecurityAnswerSalt>
    {
        public SecurityAnswerSaltValidator()
        {
            RuleFor(securityAnswerSalt => securityAnswerSalt.Salt)
                .NotEmpty()
                .NotNull();
        }
    }
}
