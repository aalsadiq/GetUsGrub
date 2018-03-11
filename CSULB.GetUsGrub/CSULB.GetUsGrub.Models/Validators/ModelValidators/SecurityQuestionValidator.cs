using FluentValidation;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityQuestionValidator</c> class.
    /// Defines rules to validate a SecurityQuestion.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionValidator : AbstractValidator<SecurityQuestion>
    {
        public SecurityQuestionValidator()
        {
            RuleFor(x => x.Question)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Answer)
                .NotEmpty()
                .NotNull();
        }
    }
}
