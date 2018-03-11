using FluentValidation;

namespace CSULB.GetUsGrub.Models
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
            RuleFor(x => x.Salt)
                .NotEmpty()
                .NotNull();
        }
    }
}
